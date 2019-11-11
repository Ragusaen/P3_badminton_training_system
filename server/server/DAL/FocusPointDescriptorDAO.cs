using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using MySql.Data.MySqlClient;

namespace Server.DAL
{
    class FocusPointDescriptorDAO
    {
        public FocusPointDescriptor Load(int ID)
        {
            var db = new DBConnection();
            var dtRow = db.ExecuteSelectQuery(
                "SELECT * FROM `focuspoint` WHERE `ID` = @ID",
                new MySqlParameter[]
                {
                    new MySqlParameter("@ÌD", ID),
                }
            ).Rows[0];

            var result = new FocusPointDescriptor {
                ID          = (int)     dtRow["ID"],
                Name        = (string)  dtRow["Name"],
                IsPrivate   = (bool)    dtRow["IsPrivate"],
                Description = (string)  dtRow["Description"],
                VideoURL    = (string)  dtRow["VideoURL"]
            };

            return result;
        }
    }
}
