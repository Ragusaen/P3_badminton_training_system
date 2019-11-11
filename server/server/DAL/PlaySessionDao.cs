using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using MySql.Data.MySqlClient;

namespace Server.DAL
{
    class PlaySessionDao
    {
        (List<PracticeSession>, List<Match>) LoadBetween(DateTime start, DateTime end)
        {
            var db = new DBConnection();

            var dt = db.ExecuteSelectQuery(
                "SELECT * FROM playsession WHERE `StartDate` BETWEEN @start AND @end" +
                "INNER JOIN practicesession ON playsession.ID=practicesession.PlaySessionID",
                new MySqlParameter[]
                {
                    new MySqlParameter("@start", start),
                    new MySqlParameter("@end", end),
                }
            );

            List<PracticeSession> practices = new List<PracticeSession>();
            List<Match> matches = new List<Match>();

            foreach (DataRow dataRow in dt.Rows)
            {
                PlaySession ps;
                if ((PlaySession.Type) dataRow[4] == PlaySession.Type.Practice)
                    ps = new PracticeSession();
                else
                    ps = new Match();

                ps.id       = (int)     dataRow["ID"];
                ps.Location = (string)  dataRow["Location"];
                ps.Start    = (DateTime)dataRow["StartDate"];
                ps.End      = (DateTime)dataRow["EndDate"]; 

                if (ps is PracticeSession practice)
                {
                    var dt1 = db.ExecuteSelectQuery("SELECT (`ID`, `Location`, `StartDate`, `EndData`, `Type`) FROM playsession WHERE `StartDate` BETWEEN @start AND @end",
                        new MySqlParameter[]
                        {
                            new MySqlParameter("@start", start),
                            new MySqlParameter("@end", end),
                        }
                    );
                }
                else
                {

                }

            }

            return (null, null);
        }
    }
}
