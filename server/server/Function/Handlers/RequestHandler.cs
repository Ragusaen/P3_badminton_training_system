using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Common.Model;
using Common.Serialization;
using NLog;
using Server.Controller;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    abstract class RequestHandler
    {
        protected Member RequestMember;
        protected static Logger _log = LogManager.GetCurrentClassLogger();
        protected byte[] OuterHandle<TRequest, TResponse>(byte[] data, Func<TRequest, member, TResponse> innerHandle) where TRequest : Request where TResponse : Response, new()
        {
            var serializer = new Serializer();
            TRequest request;
            TResponse response;

            try
            {
                request = serializer.Deserialize<TRequest>(data);
            }
            catch (XmlException)
            {
                _log.Error("Error deserializing request:\n" + Encoding.ASCII.GetString(data));

                return serializer.Serialize(new TResponse()
                {
                    Error = "Unknown deserialization error"
                });
            }

            member member = null;
            if (request is PermissionRequest pr)
                member = GetMember(pr);

            try
            {
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

                return serializer.Serialize(new TResponse()
                {
                    Error = "Unknown error handling request"
                });
            }


            LogAccess(response, member);

            try
            {
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

        private Server.DAL.member GetMember(PermissionRequest pr)
        {
            var um = new UserManager();
            return um.GetMemberFromToken(pr.Token);
        }

        public abstract byte[] Handle(byte[] data);
    }

    abstract class MiddleRequestHandler<TRequest, TResponse> : RequestHandler where TRequest : Request where TResponse : Response, new()
    {
        protected abstract TResponse InnerHandle(TRequest request, member requester);

        public override byte[] Handle(byte[] data)
        {
            return OuterHandle<TRequest, TResponse>(data, InnerHandle);
        }
    }
}
