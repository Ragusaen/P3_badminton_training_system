using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetScheduleHandler : MiddleRequestHandler<GetScheduleRequest, GetScheduleResponse>
    {
        protected override GetScheduleResponse InnerHandle(GetScheduleRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
