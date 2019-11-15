using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.Controller;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    abstract class RequestHandler
    {
        protected byte[] OuterHandle<TRequest, TResponse>(byte[] data, Func<TRequest, member, TResponse> innerHandle) where TRequest : Request where TResponse : Response
        {
            var serializer = new Serializer();

            TRequest request = serializer.Deserialize<TRequest>(data);
            member member = null;
            if (request is PermissionRequest pr)
                member = GetMember(pr);
            
            TResponse response = innerHandle(request, member);

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
