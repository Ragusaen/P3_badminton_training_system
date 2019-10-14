using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Server.Model;

namespace Server.DAL
{
    class DBReader
    {
        public List<Player> FetchPlayers()
        {
            List<Player> players = new List<Player>();
            DBConnection dbc = new DBConnection();
            MySqlParameter[] arr = new MySqlParameter[0];
            string query = "SELECT * FROM p3_db.player";

            DataTable dt = dbc.ExecuteSelectQuery(query, arr);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ID = (int)dt.Rows[i]["BadmintonPlayerId"];
                players.Add(new Player(new Member(), ID));
            }

            return players;
        }
    }
}
