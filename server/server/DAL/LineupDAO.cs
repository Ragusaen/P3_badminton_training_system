using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
    class LineupDAO
    {
        public List<int> GetMemberIDsPlayingInLeagueRound(int leagueRound, int season)
        {
            string query = "select MemberID from Position join TeamMatch on MatchID = ID where LeagueRound = @LeagueRound and Season = @Season;";
            MySqlParameter[] param = new MySqlParameter[2];
            param[0] = new MySqlParameter("@LeagueRound", leagueRound);
            param[1] = new MySqlParameter("@Season", season);
            DBConnection db = new DBConnection();
            DataTable dt = db.ExecuteSelectQuery(query, param);

            List<int> memberids = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
                memberids.Add(Convert.ToInt32(dt.Rows[i]["MemberID"]));

            return memberids;
        }
    }
}