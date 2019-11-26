using Common.Model;
using Server.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Function.Rules
{
    class ReservesRule : IRule
    {
        public int Priority { get; set; }
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();

        public List<RuleBreak> Rule(TeamMatch match)
        {
            _ruleBreaks = new List<RuleBreak>();
            CheckPlayersReserveRound(match);
            CheckPlayersReserveLastRound(match);

            return _ruleBreaks;
        }

        private List<Position> GetPlayersInLeagueRound(int leagueRound, int season, int matchId)
        {
            var db = new DatabaseEntities();
            //Get all positions in lineups this round, except the lineup that is being verified.
            var lineupPositions = db.teammatches.ToList()
                .Where(t => t.PlaySessionID != matchId && t.LeagueRound == leagueRound && t.Season == season)
                .Select(p => p.positions).ToList();

            List<Position> players = new List<Position>();
            foreach (var lineup in lineupPositions)
                foreach (var position in lineup)
                    players.Add((Position)position);

            return players;
        }


        private void CheckPlayersReserveRound(TeamMatch match)
        {
            List<Position> positions = GetPlayersInLeagueRound(match.LeagueRound, match.Season, match.ID);
            foreach (var group in match.Lineup)
            {
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    //If player is on another lineup this round, and the player is not set as reserve on the lineup getting verified
                    //and one of the other lineups the player is playing on this round.
                    if (group.Positions[i].Player != null && 
                        positions.Any(p => p.Player.Member.Id == group.Positions[i].Player.Member.Id && !p.IsExtra) && !group.Positions[i].IsExtra)
                    {
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 0, "Player is already on another lineup this round and is not set as a reserve player!"));
                    }

                    if (Lineup.PositionType.Double.HasFlag(group.Type) && 
                        group.Positions[i].Player != null && 
                        group.Positions[i].OtherPlayer != null &&
                        positions.Any(p =>p.Player.Member.Id == group.Positions[i].OtherPlayer.Member.Id && !p.IsExtra) && !group.Positions[i].OtherIsExtra)
                    {
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 1, "Player is already on another lineup this round and is not set as a reserve player!"));
                    }
                }
            }
        }

        private void CheckPlayersReserveLastRound(TeamMatch match)
        {
            List<Position> positions = GetPlayersInLeagueRound(match.LeagueRound - 1, match.Season, match.ID);
            foreach (var group in match.Lineup)
            {
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    var pos = group.Positions[i];
                    if(pos.Player != null && positions.Count(p => p.Player.Member.Id == pos.Player.Member.Id) > 2)
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 0, "Player played twice last round!"));
                    
                    if (Lineup.PositionType.Double.HasFlag(group.Type) && pos.Player != null && pos.OtherPlayer != null &&
                        positions.Count(p => p.Player.Member.Id == pos.OtherPlayer.Member.Id) > 2)
                    {
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 1, "Player played twice last round!"));
                    }
                }
            }
        }
    }
}
