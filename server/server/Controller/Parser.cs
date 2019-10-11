using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Model;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using Server.DAL;
using MySql.Data.MySqlClient;
using System.Data;

namespace Server.Controller
{
    class Parser
    {
        public void UpdatePlayers()
        {
            var players = FetchPlayersFromSQL();
            
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless");
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            IWebDriver browser = new ChromeDriver(chromeOptions);

            for (int i = 0; i < Constants.RankingUrlArray.Length; i++)
            {
                browser.Navigate().GoToUrl(Constants.RankingUrlArray[i]);

                List<IWebElement> rawRanking = ScrapeRankingsWebsite(browser);

                DistributeRankings(players, rawRanking, i);
            }
            browser.Quit();

            WritePlayersToSQL(players);
        }

        #pragma warning disable CS0618
        private List<IWebElement> ScrapeRankingsWebsite(IWebDriver driver)
        {
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(3000));

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div[4]/div[1]/div[5]/table")));
            Thread.Sleep(1000);

            return driver.FindElement(By.ClassName(Constants.RankingListElementClassName)).FindElements(By.TagName("tr")).ToList();
        }

        private void DistributeRankings(List<Player> players, List<IWebElement> rawRanking, int category)
        {
            // skips first row to avoid the title
            for (int i = 1; i < rawRanking.Count - 1; i++)
            {
                var currentRow = rawRanking[i];

                //Fetches information from the current row
                string rawPlayerid = currentRow.FindElement(By.ClassName("playerid")).GetAttribute("innerHTML");
                int badmintonPlayerID = RemoveFalseHyphen(rawPlayerid);
                int points = FetchPointsFromRow(currentRow);
                string skillLevel = FetchSkillLevelFromRow(currentRow);

                Player p2;

                if (players.Exists(p => p.BadmintonPlayerID.Equals(badmintonPlayerID)))
                {
                    p2 = players.Find(p => p.BadmintonPlayerID.Equals(badmintonPlayerID));
                    AssignPointsFromRow(p2, points, category);
                }
                else
                {
                    p2 = new Player(new Member(), badmintonPlayerID);
                    AssignPointsFromRow(p2, points, category);
                    players.Add(p2);
                }
            }
        }

        private void AssignPointsFromRow(Player p, int points, int category)
        {
            switch (category)
            {
                case (int)Constants.EnumRankings.Level:
                    p.Rankings.LevelPoints = points;
                    break;
                case (int)Constants.EnumRankings.MS:
                    p.Rankings.SinglesPoints = points;
                    break;
                case (int)Constants.EnumRankings.WS:
                    p.Rankings.SinglesPoints = points;
                    break;
                case (int)Constants.EnumRankings.MD:
                    p.Rankings.DoublesPoints = points;
                    break;
                case (int)Constants.EnumRankings.WD:
                    p.Rankings.DoublesPoints = points;
                    break;
                case (int)Constants.EnumRankings.MMD:
                    p.Rankings.MixPoints = points;
                    break;
                case (int)Constants.EnumRankings.WMD:
                    p.Rankings.MixPoints = points;
                    break;
                default:
                    throw new Exception($"Category could not be recognised. Category is: {category}");
            }
        }

        private string FetchSkillLevelFromRow(IWebElement elem)
        {
            return elem.FindElement(By.ClassName("clas")).GetAttribute("innerHTML");
        }

        private int FetchPointsFromRow(IWebElement elem)
        {
            return Int32.Parse(elem.FindElement(By.ClassName("points")).GetAttribute("innerHTML"));
        }

        private int RemoveFalseHyphen(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            byte[] newBytes = new byte[bytes.Length - 3];

            int i = 0;

            while (!(bytes[i] == 0xE2 && bytes[i + 1] == 0x80 && bytes[i + 2] == 0x91) && i < bytes.Length) // while(byte sequence does not equal the Non-Breaking-Hyphen) { ... }
                i++;
            Array.Copy(bytes, 0, newBytes, 0, i);
            Array.Copy(bytes, i + 3, newBytes, i, 2);

            return Int32.Parse(Encoding.UTF8.GetString(newBytes));
        }

        private List<Player> FetchPlayersFromSQL()
        {
            List<Player> players = new List<Player>();
            DBConnection dbc = new DBConnection();
            string query = "SELECT * FROM p3_db.player";

            MySqlParameter[] arr = new MySqlParameter[0];

            DataTable dt = dbc.ExecuteSelectQuery(query, arr);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ID = (int) dt.Rows[i]["BadmintonPlayerID"];
                players.Add(new Player(new Member(), ID));
            }

            return players;
        }
        
        private void WritePlayersToSQL(List<Player> players)
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
                                   "insert into Player(MemberID, BadmintonPlayerID) values(LAST_INSERTED_ID(), @BadmintonPlayerID);" +
                                   "insert into RankList(PlayerMemberID, MixPoints, SinglePoints, DoublePoints, OverallPoints, `Level`) " +
                                   "values(last_insert_id(), @MixPoints, @SinglePoints, @DoublePoints, @OverallPoints, @Level);";

                    Player p = players[i];
                    MySqlParameter[] sqlParameters = new MySqlParameter[8];
                    sqlParameters[0] = new MySqlParameter("@Name", p.Member.Name);
                    sqlParameters[1] = new MySqlParameter("@sex", p.Member.Sex);
                    sqlParameters[2] = new MySqlParameter("@BadmintonPlayerID", p.BadmintonPlayerID);
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