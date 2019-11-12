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

        public List<RuleBreak> Rule(Match match) //TODO: Check if rules are correct + Make an extra method to clean up code
        {
            List<RuleBreak> ruleBreaks = new List<RuleBreak>();

            List<int> memberidsRound = null;//lineupDAO.GetMemberIDsPlayingInLeagueRound(match.LeagueRound, match.Season);
            foreach (var position in match.Lineup.Positions)
            {
                if (memberidsRound.Count(p => p == position.Value.Player.Member.Id) > 2)
                    ruleBreaks.Add(new RuleBreak(position.Key, 0, "WARNING: Player is already on another lineup this round!"));
                if ((position.Value is DoublePosition dp) && memberidsRound.Count(p => p == dp.OtherPlayer.Member.Id) > 2)
                    ruleBreaks.Add(new RuleBreak(position.Key, 1, "WARNING: Player is already on another lineup this round!"));
            }

            memberidsRound = null;// lineupDAO.GetMemberIDsPlayingInLeagueRound(match.LeagueRound - 1, match.Season);
            foreach (var position in match.Lineup.Positions)
            {
                if (memberidsRound.Count(p => p == position.Value.Player.Member.Id) > 4)
                    ruleBreaks.Add(new RuleBreak(position.Key, 0, "This played played twice last round!"));
                if ((position.Value is DoublePosition dp) && memberidsRound.Count(p => p == dp.OtherPlayer.Member.Id) > 4)
                    ruleBreaks.Add(new RuleBreak(position.Key, 1, "This played played twice last round!"));
            }

            return ruleBreaks;
        }
    }
}
