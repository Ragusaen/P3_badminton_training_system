using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class SetPracticeTeamHandler : MiddleRequestHandler<SetPracticeTeamRequest, SetPracticeTeamResponse>
    {
        protected override SetPracticeTeamResponse InnerHandle(SetPracticeTeamRequest request, member requester)
        {
            if (!((Common.Model.MemberType) requester.MemberType).HasFlag(MemberType.Trainer))
                return new SetPracticeTeamResponse {AccessDenied = true};

            var db = new DatabaseEntities();
            var e = request.PracticeTeam;
            var dbPt = new practiceteam
            {
                Name = e.Name
            };
            if(e.Trainer != null)
            {
                dbPt.TrainerID = e.Trainer.Member.Id;
            }
            _log.Debug($"Created new practice team: {dbPt.Name}");
            db.practiceteams.Add(dbPt);

            db.SaveChanges();
            return new SetPracticeTeamResponse();
        }
    }
}
