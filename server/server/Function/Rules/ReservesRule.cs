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
            throw new NotImplementedException();
            /*List<int> memberidsRound = null;//TODO: GetMemberIDsPlayingInLeagueRound(match.LeagueRound, match.Season);
            foreach (var position in match.Lineup.Positions)
            {
                if (memberidsRound.Count(p => p == position.Value.Player.Member.Id) > 2)
                    _ruleBreaks.Add(new RuleBreak(position.Key, 0, "WARNING: Player is already on another lineup this round!"));
                if ((position.Value.OtherPlayer != null) && memberidsRound.Count(p => p == position.Value.OtherPlayer.Member.Id) > 2)
                    _ruleBreaks.Add(new RuleBreak(position.Key, 1, "WARNING: Player is already on another lineup this round!"));
            }

            memberidsRound = null;// TODO: GetMemberIDsPlayingInLeagueRound(match.LeagueRound - 1, match.Season);
            foreach (var position in match.Lineup.Positions)
            {
                if (memberidsRound.Count(p => p == position.Value.Player.Member.Id) > 4)
                    _ruleBreaks.Add(new RuleBreak(position.Key, 0, "This played played twice last round!"));
                if ((position.Value.OtherPlayer != null) && memberidsRound.Count(p => p == position.Value.OtherPlayer.Member.Id) > 4)
                    _ruleBreaks.Add(new RuleBreak(position.Key, 1, "This played played twice last round!"));
            }

            return _ruleBreaks;*/
        }
    }
}
