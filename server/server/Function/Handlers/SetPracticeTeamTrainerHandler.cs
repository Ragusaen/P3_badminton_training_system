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
    class SetPracticeTeamTrainerHandler : MiddleRequestHandler<SetPracticeTeamTrainerRequest, SetPracticeTeamTrainerResponse>
    {
        protected override SetPracticeTeamTrainerResponse InnerHandle(SetPracticeTeamTrainerRequest request, member requester)
        {
            if (!((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
                return new SetPracticeTeamTrainerResponse { AccessDenied = true };

            var db = new DatabaseEntities();
            var dbTeam = db.practiceteams.Find(request.PracticeTeam.Id);
            var dbTrainer = db.members.Find(request.Trainer.Member.Id);
            dbTeam.trainer = dbTrainer;
            db.SaveChanges();

            return new SetPracticeTeamTrainerResponse();
        }
    }
}
