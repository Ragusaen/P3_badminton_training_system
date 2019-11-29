using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class SetTeamMatchHandler : MiddleRequestHandler<SetTeamMatchRequest, SetTeamMatchResponse>
    {
        protected override SetTeamMatchResponse InnerHandle(SetTeamMatchRequest request, member requester)
        {
            if (!((MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
                return new SetTeamMatchResponse() { AccessDenied = true };

            var db = new DatabaseEntities();
            var e = request.TeamMatch;
            var dbTM = new teammatch()
            {
                playsession = new playsession()
                {
                    EndDate = e.End,
                    StartDate = e.Start,
                    Location = e.Location,
                    Type = (int)PlaySession.Type.Match
                },
                captain = e.Captain == null ? null : db.members.Find(e.Captain.Id),
                League = (int)e.League,
                LeagueRound = e.LeagueRound,
                OpponentName = e.OpponentName,
                TeamIndex = e.TeamIndex,
                Season = e.Season
            };
            db.teammatches.Add(dbTM);
            db.positions.AddRange(new LineUpCast().CreatePositions(e.Lineup, dbTM, db));
            db.SaveChanges();
            return new SetTeamMatchResponse();
        }
    }
}
