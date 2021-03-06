﻿using System.Linq;
using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class DeletePracticeTeamHandler : MiddleRequestHandler<DeletePracticeTeamRequest, DeletePracticeTeamResponse>
    {
        protected override DeletePracticeTeamResponse InnerHandle(DeletePracticeTeamRequest request, member requester)
        {
            if (!((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
            {
                return new DeletePracticeTeamResponse { AccessDenied = true };
            }

            var db = new DatabaseEntities();
            var team = db.practiceteams.Find(request.PracticeTeam.Id);

            if (team != null)
            {
                var players = team.players.ToList();
                var trainer = team.trainer;
                var practiceSessions = team.practicesessions.ToList();

                trainer.practiceteamstrainer.Remove(team);
                foreach (var m in players)
                {
                    m.practiceteamsplayer.Remove(team);
                }

                foreach (var ps in practiceSessions)
                {
                    ps.practiceteam = null;
                }
                db.practiceteams.Remove(team);
                _log.Debug($"Completely removed practice team {team.Name}");
            }
            else
            {
                _log.Debug($"Could not find team - ID: {request.PracticeTeam.Id}");
            }

            db.SaveChanges();
            return new DeletePracticeTeamResponse();
        }
    }
}
