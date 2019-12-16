using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class DeletePlayerPracticeTeamHandler : MiddleRequestHandler<DeletePlayerPracticeTeamRequest, DeletePlayerPracticeTeamResponse>
    {
        protected override DeletePlayerPracticeTeamResponse InnerHandle(DeletePlayerPracticeTeamRequest request, member requester)
        {
            if (!(((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer) ||
                  requester.ID == request.Player.Member.Id))
            {
                return new DeletePlayerPracticeTeamResponse { AccessDenied = true };
            }

            var db = new DatabaseEntities();
            var dbPt = db.practiceteams.Find(request.PracticeTeam.Id);
            var dbPlayer = db.members.Find(request.Player.Member.Id);
            dbPlayer.practiceteamsplayer.Remove(dbPt);
            dbPt.players.Remove(dbPlayer);
            db.SaveChanges();

            _log.Debug($"Player: {request.Player.Member.Name} removed from Practice Team: {dbPt.Name}");
            return new DeletePlayerPracticeTeamResponse();
        }
    }
}
