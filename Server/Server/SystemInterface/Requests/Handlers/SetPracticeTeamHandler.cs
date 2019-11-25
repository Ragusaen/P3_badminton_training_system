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
            {
                return new SetPracticeTeamResponse { AccessDenied = true };
            }

            var db = new DatabaseEntities();
            var dbTeam = new practiceteam
            {
                Name = request.PracticeTeam.Name,
            };
            db.practiceteams.Add(dbTeam);
            db.SaveChanges();

            return new SetPracticeTeamResponse();
        }
    }
}