using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetTokenMemberHandler : MiddleRequestHandler<GetTokenMemberRequest, GetTokenMemberResponse>
    {
        protected override GetTokenMemberResponse InnerHandle(GetTokenMemberRequest request, member requester)
        {
            return new GetTokenMemberResponse
            {
                Member = (Common.Model.Member) requester
            };
        }
    }
}
