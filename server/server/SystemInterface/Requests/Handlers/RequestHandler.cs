﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.Controller;

namespace Server.SystemInterface.Requests.Handlers
{
    abstract class RequestHandler
    {
        protected byte[] OuterHandle<TRequest, TResponse>(byte[] data, Func<TRequest, TResponse> innerHandle) where TRequest : class where TResponse : class
        {
            var serializer = new Serializer();

            TRequest request = serializer.Deserialize<TRequest>(data);

            if (request is PermissionRequest pr)
                GetPermissionLevel(pr);

            TResponse response = innerHandle(request);

            return serializer.Serialize(response);
        }

        private MemberRole.Type GetPermissionLevel(PermissionRequest pr)
        {
            var um = new UserManager();
            throw new NotImplementedException();
        }

        public abstract byte[] Handle(byte[] data);

    }

    abstract class MiddleRequestHandler<TRequest, TResponse> : RequestHandler where TRequest : class where TResponse : class
    {
        protected abstract TResponse InnerHandle(TRequest request);

        public override byte[] Handle(byte[] data)
        {
            return OuterHandle<TRequest, TResponse>(data, InnerHandle);
        }
    }
}
