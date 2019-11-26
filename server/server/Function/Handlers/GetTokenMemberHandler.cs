using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using NLog;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetTokenMemberHandler : MiddleRequestHandler<GetTokenMemberRequest, GetTokenMemberResponse>
    {
        protected override GetTokenMemberResponse InnerHandle(GetTokenMemberRequest request, member requester)
        {
            _log.Debug($"Type:{Enum.GetName(typeof(MemberType),requester.MemberType)} logged in: {requester.Name}");
            return new GetTokenMemberResponse
            {
                Member = (Common.Model.Member) requester
            };
        }
    }
}
