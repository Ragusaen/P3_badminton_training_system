using System.Linq;
using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
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
