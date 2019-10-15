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
            var pdao = new PlayerDAO();
            List<Player> players = pdao.Read().ToList();
            
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless");
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            IWebDriver browser = new ChromeDriver(chromeOptions);

            for (int i = 0; i < Constants.RankingUrlArray.Length; i++)
            {
                browser.Navigate().GoToUrl(Constants.RankingUrlArray[i]);

                List<IWebElement> rawRanking = ScrapeRankingsWebsite(browser);

                DistributeRankings(players, rawRanking, i);

                try // Checking for next page
                {
                    var nextPage = browser.FindElement(By.XPath("/html/body/form/div[4]/div[1]/div[5]/table/tbody/tr[102]/td/a"));
                    nextPage.Click();
                    Thread.Sleep(4000);

                    rawRanking = ScrapeRankingsWebsite(browser);
                    DistributeRankings(players, rawRanking, i);
                }
                catch (Exception) { }
            }
            browser.Quit();

            pdao.Write(players);
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

                
                // Fetches information from the current row
                string rawPlayerid = currentRow.FindElement(By.ClassName("playerid")).GetAttribute("innerHTML");
                int BadmintonPlayerId = RemoveFalseHyphen(rawPlayerid);
                int points = FetchPointsFromRow(currentRow);
                string level = FetchSkillLevelFromRow(currentRow);

                Player p2;

                if (players.Exists(p => p.BadmintonPlayerId.Equals(BadmintonPlayerId)))
                {
                    p2 = players.Find(p => p.BadmintonPlayerId.Equals(BadmintonPlayerId));
                    AssignPointsFromRow(p2, points, level, category);
                }
                else
                {
                    p2 = new Player(new Member(), BadmintonPlayerId);
                    string name = new string(currentRow.FindElement(By.ClassName("name")).Text.TakeWhile(p => p != ',').ToArray());
                    p2.Member.Name = name;
                    Console.WriteLine(p2.Member.Name);
                    AssignPointsFromRow(p2, points, level, category);
                    players.Add(p2);
                }
            }
        }

        private void AssignPointsFromRow(Player p, int points, string level, int category)
        {
            switch (category)
            {
                case (int)Constants.EnumRankings.Level:
                    p.Rankings.LevelPoints = points;
                    p.Rankings.Level = level;
                    break;
                case (int)Constants.EnumRankings.MS:
                    p.Rankings.SinglesPoints = points;
                    p.Member.Sex = 0;
                    break;
                case (int)Constants.EnumRankings.WS:
                    p.Rankings.SinglesPoints = points;
                    p.Member.Sex = 1;
                    break;
                case (int)Constants.EnumRankings.MD:
                    p.Rankings.DoublesPoints = points;
                    p.Member.Sex = 0;
                    break;
                case (int)Constants.EnumRankings.WD:
                    p.Rankings.DoublesPoints = points;
                    p.Member.Sex = 1;
                    break;
                case (int)Constants.EnumRankings.MMD:
                    p.Rankings.MixPoints = points;
                    p.Member.Sex = 0;
                    break;
                case (int)Constants.EnumRankings.WMD:
                    p.Rankings.MixPoints = points;
                    p.Member.Sex = 1;
                    break;
                default:
                    throw new ArgumentException($"Category could not be recognised. Category is: {category}");
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
    }
}