using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Server.DAL;
using Server.Model;

namespace Server.Model.Rules
{
    class ReservesRule : IRule
    {
        public string ErrorMessage { get; set; }
        public List<RuleBreak> RuleBreaks { get; set; }

        public List<RuleBreak> Rule(Lineup lineup)
        {
            string query = "select MemberId from Player join PlayerMatch on MemberID = PlayerMemberID where LeagueRound = @LeagueRound";
            MySqlParameter[] param = new MySqlParameter[1];
            param[0] = new MySqlParameter("@LeagueRound", lineup.Round);
            DBConnection db = new DBConnection();
            DataTable dt = db.ExecuteSelectQuery(query, param);

            List<int> memberids = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                memberids.Add(Convert.ToInt32(dt.Rows[i][0]));
            }
            ErrorMessage = "Warning the player is allready on a lineup this round";
            CheckPlayer(lineup, memberids, 2);

            param[0] = new MySqlParameter("@LeagueRound", lineup.Round - 1);
            DataTable dt2 = db.ExecuteSelectQuery(query, param);

            List<int> memberids2 = new List<int>();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                memberids.Add(Convert.ToInt32(dt2.Rows[i][0]));
            }
            ErrorMessage = "Error this player played twice last round";
            CheckPlayer(lineup, memberids, 4);

            return RuleBreaks;  
        }
        public void CheckPlayer(Lineup lineup, List<int> memberids, int number) 
        {
            int Count = 0;
            foreach (IPosition positions in lineup.Positions)
            {
                foreach (Player player in positions.Player)
                {
                    Count = 0;
                    foreach (int id in memberids)
                    {
                        if (player.Member.Id == id)
                            Count++;
                    }
                    if (Count >= number)
                        RuleBreaks.Add(new RuleBreak(player, ErrorMessage));
                }
            }
        }
    }
}
