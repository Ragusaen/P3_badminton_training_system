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
    class PlayerDAO : IDAO<Player>
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

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
            string query = "update RankList set MixPoints=@MixPoints, SinglePoints=@SinglePoints, DoublesPoints=@DoublesPoints, " +
                           "LevelPoints=@LevelPoints, Level=@Level where PlayerMemberID=@ID;";
            MySqlParameter[] sqlParameters = new MySqlParameter[6];
            sqlParameters[0] = new MySqlParameter("@MixPoints", p.Rankings.MixPoints);
            sqlParameters[1] = new MySqlParameter("@SinglePoints", p.Rankings.SinglesPoints);
            sqlParameters[2] = new MySqlParameter("@DoublesPoints", p.Rankings.DoublesPoints);
            sqlParameters[3] = new MySqlParameter("@LevelPoints", p.Rankings.LevelPoints);
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
                           "insert into RankList(PlayerMemberID, MixPoints, SinglePoints, DoublesPoints, LevelPoints, `Level`) " +
                           "values(last_insert_id(), @MixPoints, @SinglePoints, @DoublesPoints, @LevelPoints, @Level);";

            MySqlParameter[] sqlParameters = new MySqlParameter[8];
            sqlParameters[0] = new MySqlParameter("@Name", p.Member.Name);
            sqlParameters[1] = new MySqlParameter("@sex", p.Member.Sex);
            sqlParameters[2] = new MySqlParameter("@BadmintonPlayerId", p.BadmintonPlayerId);
            sqlParameters[3] = new MySqlParameter("@MixPoints", p.Rankings.MixPoints);
            sqlParameters[4] = new MySqlParameter("@SinglePoints", p.Rankings.SinglesPoints);
            sqlParameters[5] = new MySqlParameter("@DoublesPoints", p.Rankings.DoublesPoints);
            sqlParameters[6] = new MySqlParameter("@LevelPoints", p.Rankings.LevelPoints);
            sqlParameters[7] = new MySqlParameter("@Level", p.Rankings.Level);
            DBConnection db = new DBConnection();
            db.ExecuteInsertUpdateDeleteQuery(query, sqlParameters);
        }

        public void WriteSingle(Player p)
        {
            if (p.Member.Id == 0)
                p.Member.Id = GetMemberIDFromBadmintonPlayerID(p.BadmintonPlayerId);

            if (p.Member.Id == 0)
                InsertPlayerRow(p);
            else
                UpdatePlayerRow(p);
        }
        
        public void WriteMany(IEnumerable<Player> players)
        {
            foreach (var p in players)
                WriteSingle(p);
        }

        public Player ReadSingle(string id)
        {
            var dbc = new DBConnection();
            var arr = new MySqlParameter[0];

            string query = $"SELECT * FROM p3_db.player WHERE MemberID = {id};";
            DataTable dt = dbc.ExecuteSelectQuery(query, arr);
            int BadmintonPlayerID = (int)dt.Rows[0]["BadmintonPlayerID"];

            query = $"SELECT * FROM p3_db.member WHERE ID = {id};";
            dt = dbc.ExecuteSelectQuery(query, arr);
            string name = (string) dt.Rows[0]["Name"];
            int sex = (int) dt.Rows[0]["Sex"];

            query = $"SELECT * FROM p3_db.ranklist WHERE PlayerMemberID = {id};";
            dt = dbc.ExecuteSelectQuery(query, arr);
            var ranking = new PlayerRanking();
            ranking.LevelPoints = (int)dt.Rows[0]["LevelPoints"];
            ranking.SinglesPoints = (int)dt.Rows[0]["SinglePoints"];
            ranking.DoublesPoints = (int)dt.Rows[0]["DoublesPoints"];
            ranking.MixPoints = (int)dt.Rows[0]["MixPoints"];
            if (!(dt.Rows[0]["Level"] is System.DBNull))
            {
                ranking.Level = (string)dt.Rows[0]["Level"];
            }

            Player p = new Player();
            p.Member.Id = int.Parse(id);
            p.Rankings = ranking;
            return p;
        }

        public IEnumerable<Player> ReadAll()
        {
            List<Player> players = new List<Player>();
            DBConnection dbc = new DBConnection();
            MySqlParameter[] arr = new MySqlParameter[0];
            string query = "SELECT * FROM p3_db.player";
            var pt = dbc.ExecuteSelectQuery(query, arr).Rows;

            int[] idArr = new int[pt.Count];

            for (int i = 0; i < pt.Count; i++)
            {
                idArr[i] = (int)pt[i]["MemberID"];
            }
            
            foreach (var id in idArr)
            {
                players.Add(ReadSingle(id.ToString()));
            }

            return players;
        }
    }
}
