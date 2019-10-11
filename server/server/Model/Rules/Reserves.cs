using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using server.DAL;
using Server.Model;

namespace server.Model.Rules
{
    class Reserves : IRule
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
            
            param[0] = new MySqlParameter("@LeagueRound", lineup.Round - 1);
            DataTable dt2 = db.ExecuteSelectQuery(query, param);

            return RuleBreaks;  
        }
    }
}
