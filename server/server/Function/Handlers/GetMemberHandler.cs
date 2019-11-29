﻿using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class GetMemberHandler : MiddleRequestHandler<GetMemberRequest, GetMemberResponse>
    {
        protected override GetMemberResponse InnerHandle(GetMemberRequest request, member requester)
        {
            var db = new DatabaseEntities();
            return new GetMemberResponse {Member = (Common.Model.Member) db.members.Find(request.Id)};
        }
    }
}
