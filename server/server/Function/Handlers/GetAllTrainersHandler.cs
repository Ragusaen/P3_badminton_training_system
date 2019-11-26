using Common.Model;
using Common.Serialization;
using Server.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemInterface.Requests.Handlers
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
