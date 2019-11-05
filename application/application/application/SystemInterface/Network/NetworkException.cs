using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.SystemInterface.Network
{
    class FailedToConnectToServerException : Exception
    {
        public FailedToConnectToServerException(string msg) : base(msg)
        {

        }
    }

    class InvalidRequestException : Exception
    {
        public InvalidRequestException(string msg) : base(msg)
        {

        }
    }

    class NotConnectedToServerException : Exception
    {
        public NotConnectedToServerException(string msg) : base(msg)
        {

        }
    }
}
