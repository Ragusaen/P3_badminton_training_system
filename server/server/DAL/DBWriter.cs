using MySql.Data.MySqlClient;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
    class DBWriter
    {
        public void WritePlayers(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                bool PlayerIsInDB = false;

                if (PlayerIsInDB)
                {

                }
                else
                {

                    string query = "insert into `Member`(`Name`, Sex) values(@Name, @Sex);" +
                                   "insert into Player(MemberID, BadmintonPlayerId) values(LAST_INSERT_ID(), @BadmintonPlayerId);" +
                                   "insert into RankList(PlayerMemberID, MixPoints, SinglePoints, DoublePoints, OverallPoints, `Level`) " +
                                   "values(last_insert_id(), @MixPoints, @SinglePoints, @DoublePoints, @OverallPoints, @Level);";

                    Player p = players[i];
                    MySqlParameter[] sqlParameters = new MySqlParameter[8];
                    sqlParameters[0] = new MySqlParameter("@Name", p.Member.Name);
                    sqlParameters[1] = new MySqlParameter("@sex", p.Member.Sex);
                    sqlParameters[2] = new MySqlParameter("@BadmintonPlayerId", p.BadmintonPlayerId);
                    sqlParameters[3] = new MySqlParameter("@MixPoints", p.Rankings.MixPoints);
                    sqlParameters[4] = new MySqlParameter("@SinglePoints", p.Rankings.SinglesPoints);
                    sqlParameters[5] = new MySqlParameter("@DoublePoints", p.Rankings.DoublesPoints);
                    sqlParameters[6] = new MySqlParameter("@Overallpoints", p.Rankings.LevelPoints);
                    sqlParameters[7] = new MySqlParameter("@Level", p.Rankings.Level);
                    DBConnection db = new DBConnection();
                    bool res = db.ExecuteInsertUpdateDeleteQuery(query, sqlParameters);

                }
            }
        }
    }
}
