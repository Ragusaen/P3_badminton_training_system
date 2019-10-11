using MySql.Data.MySqlClient;
using Server.DAL;
using Server.Controller;
using System;
using System.Data;
using System.Data.SqlClient;
using Server.Controller;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();

            parser.UpdatePlayers();

            /*string query = "insert into `member`(Name, Sex) values(@Name, @Sex)";
            MySqlParameter[] sqlParameters = new MySqlParameter[2];
            sqlParameters[0] = new MySqlParameter("@Name", "Test");
            sqlParameters[1] = new MySqlParameter("@Sex", "M");
            DBConnection db = new DBConnection();
            bool res = db.ExecuteInsertUpdateDeleteQuery(query, sqlParameters);*/
        }
    }
}
