using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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

            TResponse response = innerHandle(request);

            return serializer.Serialize(response);
        }

    }

    abstract class MiddleRequestHandler<TRequest, TResponse> : RequestHandler where TRequest : class where TResponse : class
    {
        protected abstract TResponse InnerHandle(TRequest request);

        public byte[] Handle(byte[] data)
        {
            return OuterHandle<TRequest, TResponse>(data, InnerHandle);
        }
    }
}
