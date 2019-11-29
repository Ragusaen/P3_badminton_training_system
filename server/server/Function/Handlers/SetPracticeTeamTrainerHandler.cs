using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
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
