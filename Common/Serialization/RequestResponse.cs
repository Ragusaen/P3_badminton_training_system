using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Serialization
{
    public abstract class Request
    {
    }

    public abstract class Response 
    {
        // If not null it indicates an error
        public string Error = null;
    }
}
