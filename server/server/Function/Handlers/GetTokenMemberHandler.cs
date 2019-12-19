using System;
using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
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
