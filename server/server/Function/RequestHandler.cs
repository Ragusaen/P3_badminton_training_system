using System;
using System.Diagnostics;
using System.Text;
using System.Xml;
using Common.Model;
using Common.Serialization;
using NLog;
using Server.DAL;

namespace Server.Function.Handlers
{
    /// <summary>
    /// This is the super-class for all request handlers
    /// </summary>
    abstract class RequestHandler
    {
        // The member that send the request
        protected Member RequestMember;

        protected static Logger _log = LogManager.GetCurrentClassLogger();
        
        /// <summary>
        /// Handle the overall data serialization and deserialization. This method handles the parts of request handling that is
        /// generic for all requests.
        /// </summary>
        /// <typeparam name="TRequest"> The request class type</typeparam>
        /// <typeparam name="TResponse"> The response class type</typeparam>
        /// <param name="data"> The raw data</param>
        /// <param name="innerHandle"> The inner method to use for handling the request</param>
        /// <returns> The response in raw bytes</returns>
        protected byte[] OuterHandle<TRequest, TResponse>(byte[] data, Func<TRequest, member, TResponse> innerHandle) where TRequest : Request where TResponse : Response, new()
        {
            var serializer = new Serializer();
            TRequest request;
            TResponse response;

            try
            {
                // Deserialize the request into the excepted request class
                request = serializer.Deserialize<TRequest>(data);
            }
            catch (XmlException)
            {
                _log.Error("Error deserializing request:\n" + Encoding.ASCII.GetString(data));

                // Return an error
                return serializer.Serialize(new TResponse()
                {
                    Error = "Unknown deserialization error"
                });
            }

            // If the request is a permission request, get the member that send it
            member member = null;
            if (request is PermissionRequest pr)
                member = GetMember(pr);

            try
            {
                // Use the inner handler to handle the request
                response = innerHandle(request, member);
            }
            catch (Exception e)
            {
                string msg = "";
                foreach (var fi in typeof(TRequest).GetFields())
                {
                    msg += fi.Name + ": " + fi.GetValue(request) + "\n";
                }
                _log.Error("Unknown error handling request:\nFull Name: " + typeof(TRequest).FullName + "\n" + e.Message + "\n" + e.InnerException?.Message);

                // Return an error
                return serializer.Serialize(new TResponse()
                {
                    Error = "Unknown error handling request"
                });
            }


            LogAccess(response, member);

            try
            {
                // Serialize the response
                return serializer.Serialize(response);
            }
            catch (XmlException)
            {
                var msg = $"Error serializing {response} to type {typeof(TResponse).FullName}\n";
                foreach (var fieldInfo in typeof(TResponse).GetFields())
                {
                    msg += fieldInfo.Name + ": " + fieldInfo.GetValue(response) + "\n";
                }
                _log.Error(msg);

                // Return an error
                return serializer.Serialize(new TResponse()
                {
                    Error = "Error serializing response"
                });
            }
        }

        [Conditional("DEBUG")]
        private void LogAccess(Response response, member member)
        {
            if (response is PermissionResponse r && r.AccessDenied)
            {
                if (RequestMember == null)
                {
                    _log.Error($"{this.GetType().Name} Access Denied - requester type: {Enum.GetName(typeof(MemberType), member.MemberType)}. " +
                               $"Requester id {member.ID} -> {member.Name}");
                }
                else
                {
                    _log.Error($"{this.GetType()} Access Denied - requester type: {Enum.GetName(typeof(MemberType), member.MemberType)}. " +
                               $"Subject id: {RequestMember.Id} | Requester id {member.ID} -> {member.Name}");
                }
            }
        }

        /// <summary>
        /// Get the database member from the permission requests access token
        /// </summary>
        private Server.DAL.member GetMember(PermissionRequest pr)
        {
            var um = new AccountManager();
            return um.GetMemberFromToken(pr.Token);
        }

        public abstract byte[] Handle(byte[] data);
    }

    /// <summary>
    /// This class is for making the derived request handler use generic request and reponses, as this cannot be done in the super-class
    /// RequestHandler.
    /// </summary>
    abstract class MiddleRequestHandler<TRequest, TResponse> : RequestHandler where TRequest : Request where TResponse : Response, new()
    {
        /// <summary>
        /// This is the method which each handler should override to define the actions needed to handle the request.
        /// </summary>
        /// <param name="request">The request send by the client</param>
        /// <param name="requester">The member that send the request</param>
        /// <returns>The response to the client</returns>
        protected abstract TResponse InnerHandle(TRequest request, member requester);

        /// <summary>
        /// This method simple calls the other handle with the inner handle as a parameter. 
        /// </summary>
        /// <param name="data"> The raw request data to handle</param>
        /// <returns> The raw response data to send to client</returns>
        public override byte[] Handle(byte[] data)
        {
            return OuterHandle<TRequest, TResponse>(data, InnerHandle);
        }
    }
}
