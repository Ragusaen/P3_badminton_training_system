using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetAllPlayersHandler : MiddleRequestHandler<GetAllPlayersRequest, GetAllPlayersResponse>
    {
        protected override GetAllPlayersResponse InnerHandle(GetAllPlayersRequest request, member requester)
        {
            throw new NotImplementedException();
        }
    }
}
