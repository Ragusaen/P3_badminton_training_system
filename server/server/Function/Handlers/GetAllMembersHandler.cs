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
    class GetAllMembersHandler : MiddleRequestHandler<GetAllMembersRequest, GetAllMembersResponse>
    {
        protected override GetAllMembersResponse InnerHandle(GetAllMembersRequest request, member requester)
        {
            var db = new DatabaseEntities();
            return new GetAllMembersResponse() {Members = db.members.ToList().Select(m => (Member)m).ToList()};
        }
    }
}
