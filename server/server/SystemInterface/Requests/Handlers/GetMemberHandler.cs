using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
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
