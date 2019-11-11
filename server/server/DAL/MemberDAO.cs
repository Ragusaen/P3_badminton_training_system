using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using MySql.Data.MySqlClient;

namespace Server.DAL
{
    class MemberDAO
    {
        public bool Create(string username, MemberRole.Type memberType, string name, Sex sex, int? badmintonPlayerId)
        {
            string findUser = "(SELECT `Username` FROM `account` WHERE `Username`= @username)";
            if (username == null)
                findUser = "NULL";

            string query =
                "INSERT INTO `member`(`MemberTypeID`, `Username`, `Name`, `Status`, `Sex`, `BadmintonPlayerID`)" +
                "VALUES( " +
                "(SELECT `ID` FROM `membertype` WHERE `Description`= @membertype)," +
                findUser + "," +
                "@name, " +
                "@sex," +
                "@badmintonPlayerId);";

            var db = new DBConnection();

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@membertype", memberType),
                new MySqlParameter("@name", name),
                new MySqlParameter("@sex", (int) sex),
                new MySqlParameter("@badmintonPlayerId",
                    badmintonPlayerId.HasValue ? (object) badmintonPlayerId.Value : null),
                new MySqlParameter("@username", username)
            };

            var b = db.ExecuteInsertUpdateDeleteQuery(query, parameters);

            return b;
        }
    }
}
