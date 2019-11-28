using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Serialization
{
    public class Request
    {
    }

    public class Response
    {
        // If not null it indicates an error
        public string Error = null;
    }
}
