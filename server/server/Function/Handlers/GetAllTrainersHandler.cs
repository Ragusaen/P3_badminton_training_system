using System.Linq;
using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class GetAllTrainersHandler : MiddleRequestHandler<GetAllTrainersRequest, GetAllTrainersResponse>
    {
        protected override GetAllTrainersResponse InnerHandle(GetAllTrainersRequest request, member requester)
        {
            var db = new DatabaseEntities();

            return new GetAllTrainersResponse()
            {
                Trainers = db.members.Where(p => (p.MemberType & (int)MemberType.Trainer) != 0).ToList().Select(p => (Trainer)p).ToList()
            };
        }
    }
}
