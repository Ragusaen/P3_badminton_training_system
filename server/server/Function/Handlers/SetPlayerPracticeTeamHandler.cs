﻿using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class SetPlayerPracticeTeamHandler : MiddleRequestHandler<SetPlayerPracticeTeamRequest, SetPlayerPracticeTeamResponse>
    {
        protected override SetPlayerPracticeTeamResponse InnerHandle(SetPlayerPracticeTeamRequest request, member requester)
        {
            if (!(((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer) ||
                  requester.ID == request.Player.Member.Id))
            {
                return new SetPlayerPracticeTeamResponse { AccessDenied = true };
            }

            var db = new DatabaseEntities();
            var dbPlayer = db.members.Find(request.Player.Member.Id);
            var dbPt = db.practiceteams.Find(request.PracticeTeam.Id);
            dbPlayer.practiceteamsplayer.Add(dbPt);
            db.SaveChanges();

            _log.Debug($"Player: {dbPlayer.Name} received new team: {request.PracticeTeam.Name}");
            return new SetPlayerPracticeTeamResponse();
        }
    }
}
