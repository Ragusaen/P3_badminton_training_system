using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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
        protected byte[] OuterHandle<TRequest, TResponse>(byte[] data, Func<TRequest, member, TResponse> innerHandle) where TRequest : Request where TResponse : Response
        {
            var serializer = new Serializer();

            TRequest request = serializer.Deserialize<TRequest>(data);
            member member = null;
            if (request is PermissionRequest pr)
                member = GetMember(pr);
            
            TResponse response = innerHandle(request, member);

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

            return serializer.Serialize(response);
        }

        private Server.DAL.member GetMember(PermissionRequest pr)
        {
            var um = new UserManager();
            return um.GetMemberFromToken(pr.Token);
        }

        public abstract byte[] Handle(byte[] data);
    }

    abstract class MiddleRequestHandler<TRequest, TResponse> : RequestHandler where TRequest : Request where TResponse : Response
    {
        protected abstract TResponse InnerHandle(TRequest request, member requester);

        public override byte[] Handle(byte[] data)
        {
            return OuterHandle<TRequest, TResponse>(data, InnerHandle);
        }
    }
}
