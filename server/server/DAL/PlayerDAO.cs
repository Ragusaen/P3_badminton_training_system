using MySql.Data.MySqlClient;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Server.DAL
{
    class PlayerDAO : IDAOEnumerable<Player>
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public IEnumerable<Player> Read()
        {
            List<Player> players = new List<Player>();
            DBConnection dbc = new DBConnection();
            MySqlParameter[] arr = new MySqlParameter[0];
            string query = "SELECT * FROM p3_db.player";

            DataTable playersTable = dbc.ExecuteSelectQuery(query, arr);

            for (int i = 0; i < playersTable.Rows.Count; i++)
            {
                int badmintonPlayerID = (int)playersTable.Rows[i]["BadmintonPlayerID"];
                int memberID = (int)playersTable.Rows[i]["MemberID"];

                query = "select `Name`, Sex from `Member` where ID = @ID;";
                arr = new MySqlParameter[1];
                arr[0] = new MySqlParameter("@ID", memberID);

                DataRow memberRow = dbc.ExecuteSelectQuery(query, arr).Rows[0];

                players.Add(new Player(new Member(), badmintonPlayerID));
                players[i].Member.Name = (string) memberRow["Name"];
                players[i].Member.Sex = (int) memberRow["Sex"];
            }

            return players;
        }

        public void Write(IEnumerable<Player> players)
        {
            for (int i = 0; i < players.Count(); i++)
            {
                Player p = players.ElementAt(i);
                if(p.Member.Id == 0)
                    p.Member.Id = GetMemberIDFromBadmintonPlayerID(p.BadmintonPlayerId);

                if (p.Member.Id == 0)
                    InsertPlayerRow(p);
                else
                    UpdatePlayerRow(p);
            }
        }

        private int GetMemberIDFromBadmintonPlayerID(int badmintonPlayerId)
        {
            MySqlParameter[] param = new MySqlParameter[1];
            string query = "select MemberID from Player where BadmintonPlayerID = @BadmintonPlayerID";
            param[0] = new MySqlParameter("@BadmintonPlayerID", badmintonPlayerId);
            DBConnection db = new DBConnection();
            int id = 0;
            DataTable dt = db.ExecuteSelectQuery(query, param);
            if (dt.Rows.Count > 0)
                id = (int) dt.Rows[0][0];
            return id;
        }

        private void UpdatePlayerRow(Player p)
        {
            _log.Debug("Updating player in DB: {0}, BadmintonPlayerID: {1}", p.Member.Name, p.BadmintonPlayerId);
            string query = "update RankList set MixPoints=@MixPoints, SinglePoints=@SinglePoints, DoublePoints=@DoublePoints, " +
                           "OverallPoints=@OverallPoints, Level=@Level where PlayerMemberID=@ID;";
            MySqlParameter[] sqlParameters = new MySqlParameter[6];
            sqlParameters[0] = new MySqlParameter("@MixPoints", p.Rankings.MixPoints);
            sqlParameters[1] = new MySqlParameter("@SinglePoints", p.Rankings.SinglesPoints);
            sqlParameters[2] = new MySqlParameter("@DoublePoints", p.Rankings.DoublesPoints);
            sqlParameters[3] = new MySqlParameter("@OverallPoints", p.Rankings.LevelPoints);
            sqlParameters[4] = new MySqlParameter("@Level", p.Rankings.Level);
            sqlParameters[5] = new MySqlParameter("@ID", p.Member.Id);
            DBConnection db = new DBConnection();
            db.ExecuteInsertUpdateDeleteQuery(query, sqlParameters);
        }

        private void InsertPlayerRow(Player p)
        {
            _log.Debug("Inserting new player in DB: {0}, BadmintonPlayerID: {1}", p.Member.Name, p.BadmintonPlayerId);

            string query = "insert into `Member`(`Name`, Sex) values(@Name, @Sex);" +
                           "insert into Player(MemberID, BadmintonPlayerId) values(LAST_INSERT_ID(), @BadmintonPlayerId);" +
                           "insert into RankList(PlayerMemberID, MixPoints, SinglePoints, DoublePoints, OverallPoints, `Level`) " +
                           "values(last_insert_id(), @MixPoints, @SinglePoints, @DoublePoints, @OverallPoints, @Level);";

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
            db.ExecuteInsertUpdateDeleteQuery(query, sqlParameters);
        }
    }
}
