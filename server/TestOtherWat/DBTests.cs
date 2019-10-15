using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using Server.DAL;

namespace Test
{
    [TestClass]
    public class DBTests : DBResetter
    {
        [TestMethod]
        public void CanGetConnectionsString()
        {
            Assert.IsNotNull(ConfigurationManager.ConnectionStrings["DBConnString"]);
        }

        [TestMethod]
        public void RetrievePlayer_john_doe_Returns_sex_0_id_0()
        {
            DBConnection db = new DBConnection();

            DataTable actual = db.ExecuteSelectQuery("SELECT * FROM `member`",
                new MySqlParameter[] {});
            var ia = actual.Rows[0].ItemArray;

            Assert.AreEqual(1, (int)ia[0]);
            Assert.AreEqual("John Doe", (string)ia[1]);
            Assert.AreEqual(0, (int)ia[2]);
        }

        [TestMethod]
        public void NewPlayerGetsCorrectID_jakob_andersen()
        {
            DBConnection db = new DBConnection();

            db.ExecuteInsertUpdateDeleteQuery("INSERT INTO `member` (`Name`, `Sex`) VALUES (@name, @sex);",
                                              new MySqlParameter[] {
                                                    new MySqlParameter("@name", "Jakob Andersen"),
                                                    new MySqlParameter("@sex", 0)
                                              });

            DataTable dt = db.ExecuteSelectQuery("SELECT `ID` FROM `member` WHERE `Name`=@name",
                                  new MySqlParameter[] {
                                      new MySqlParameter("@name", "Jakob Andersen")
                                  });

            Assert.AreEqual(4, (int)dt.Rows[0].ItemArray[0]);
        }

        [TestMethod]
        public void NewPlayerGetsCorrectID2_jorn_abakus()
        {
            DBConnection db = new DBConnection();

            db.ExecuteInsertUpdateDeleteQuery("INSERT INTO `member` (`Name`, `Sex`) VALUES (@name, @sex);",
                                              new MySqlParameter[] {
                                                    new MySqlParameter("@name", "Jorn Abakus"),
                                                    new MySqlParameter("@sex", 0)
                                              });

            DataTable dt = db.ExecuteSelectQuery("SELECT `ID` FROM `member` WHERE `Name`=@name",
                                  new MySqlParameter[] {
                                      new MySqlParameter("@name", "Jorn Abakus")
                                  });

            Assert.AreEqual(4, (int)dt.Rows[0].ItemArray[0]);
        }
    }
}
