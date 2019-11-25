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
    class SetPracticeTeamHandler : MiddleRequestHandler<SetPracticeTeamRequest, SetPracticeTeamResponse>
    {
        protected override SetPracticeTeamResponse InnerHandle(SetPracticeTeamRequest request, member requester)
        {
            if (!((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
                return new SetPracticeTeamResponse();

            var db = new DatabaseEntities();
            var e = request.PracticeTeam;
            var dbPt = new practiceteam
            {
                 Name = e.Name,
            };
            db.practiceteams.Add(dbPt);

            db.SaveChanges();
            return new SetPracticeTeamResponse();
        }
    }
}
