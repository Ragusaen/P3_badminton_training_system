using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Threading;
using Common.Model;
using NLog;

namespace Server.Controller
{
    class Parser
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public const string RankingListElementClassName = "RankingListGrid";

        public enum Rankings { Level = 0, MS, WS, MD, WD, MXD, WXD }
        public static int RankingsCount = 7;
        public static string RankingRootUrl = "https://www.badmintonplayer.dk/DBF/Ranglister/#287,2019,,0,,,1492,0,,,,15,,,,0,,,,,,";
        public static string[] Categories = { "Level", "MS", "WS", "MD", "WD", "MXD", "WXD" };

        public void UpdatePlayers()
        {
            _log.Debug("UpdatePlayers startup");
            var chromeOptions = new ChromeOptions();
            // chromeOptions.AddArguments("--headless"); 
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            IWebDriver browser = new ChromeDriver(chromeOptions);
            FindRootRankList(browser);

            var players = new List<Player>();
            for (int i = 0; i < 1/*Constants.RankingsCount*/; i++)
            {
                _log.Debug("Scraping category: {category}", Categories[i]);
                string nextCategoryXPath = $"/html/body/form/div[4]/div[1]/div[5]/div/div[{i + 1}]/a";
                browser.FindElement(By.XPath(nextCategoryXPath)).Click();
                WaitForPageLoad();

                List<IWebElement> rawRanking = ScrapeRankingsTable(browser);

                DistributeRankings(players, rawRanking, i);

                try // Checking for next page
                {
                    var nextPage = browser.FindElement(By.XPath("/html/body/form/div[4]/div[1]/div[5]/table/tbody/tr[102]/td/a"));
                    nextPage.Click();
                    WaitForPageLoad();

                    _log.Debug("Scraping page 2 for category: {category}", Categories[i]);
                    rawRanking = ScrapeRankingsTable(browser);
                    DistributeRankings(players, rawRanking, i);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch (Exception) { }
            }
            browser.Quit();

            
        }

        private void FindRootRankList(IWebDriver browser)
        {
            browser.Navigate().GoToUrl(RankingRootUrl);
            WaitForPageLoad();

            string xpath = null;
            bool correctVersion = false;
            int i = 0;
            while (!correctVersion)
            {
                xpath = $"/html/body/form/div[4]/div[1]/div[2]/div[6]/select/option[{++i}]";
                correctVersion = browser.FindElement(By.XPath(xpath)).Text.Contains("senior");
            }

            _log.Debug("Using rank list version: {0}", browser.FindElement(By.XPath(xpath)).Text);
            browser.FindElement(By.XPath(xpath)).Click();

            browser.FindElement(By.Id("LinkButtonSearch")).Click();
            WaitForPageLoad();
        }

        private List<IWebElement> ScrapeRankingsTable(IWebDriver driver)
        {
            WaitForPageLoad();
            return driver.FindElement(By.ClassName(RankingListElementClassName)).FindElements(By.TagName("tr")).ToList();
        }

        private void DistributeRankings(List<Player> players, List<IWebElement> rawRanking, int category)
        {
            // skips first row to avoid the title
            for (int i = 1; i < rawRanking.Count - 1; i++)
            {
                var currentRow = rawRanking[i];
                
                // Fetches information from the current row
                string rawPlayerId = currentRow.FindElement(By.ClassName("playerid")).GetAttribute("innerHTML");
                int badmintonPlayerId = RemoveFalseHyphen(rawPlayerId);
                int points = FetchPointsFromRow(currentRow);
                string ageAndLevel = FetchSkillLevelFromRow(currentRow);

                Player p2 = new Player();
                PlayerRanking.AgeGroup ageGroup = FetchAgeGroup(ageAndLevel);
                PlayerRanking.LevelGroup levelGroup = FetchLevelGroup(ageAndLevel);

                if (players.Exists(p => p.BadmintonPlayerId.Equals(badmintonPlayerId)))
                {
                    p2 = players.Find(p => p.BadmintonPlayerId.Equals(badmintonPlayerId));
                    UpdateRankingsFromRow(p2.Rankings, points, ageGroup, levelGroup, category);
                }
                else
                {
                    string name = new string(currentRow.FindElement(By.ClassName("name")).Text.TakeWhile(p => p != ',').ToArray());
                    int sex = 0;
                    var playerRanking = new PlayerRanking();
                    UpdateRankingsFromRow(playerRanking, points, ageGroup, levelGroup, category);
                    
                    if (category == 2 || category == 4 || category == 6)
                    {
                        sex = 1;
                    }

                    Player player = new Player();
                    player.Rankings = playerRanking;
                    players.Add(player);
                }
            }
        }

        private PlayerRanking.AgeGroup FetchAgeGroup(string a)
        {
            var ageDict = new Dictionary<string, PlayerRanking.AgeGroup>
            {
                {"U9", PlayerRanking.AgeGroup.U9},
                {"U11", PlayerRanking.AgeGroup.U11},
                {"U13", PlayerRanking.AgeGroup.U13},
                {"U15", PlayerRanking.AgeGroup.U15},
                {"U17", PlayerRanking.AgeGroup.U17},
                {"U19", PlayerRanking.AgeGroup.U19},
                {"SEN", PlayerRanking.AgeGroup.Senior}
            };

            return ageDict[a.Split(' ').First()];
        }

        private PlayerRanking.LevelGroup FetchLevelGroup(string a)
        {

            var levelDict = new Dictionary<string, PlayerRanking.LevelGroup>
            {
                {"D", PlayerRanking.LevelGroup.D},
                {"C", PlayerRanking.LevelGroup.C},
                {"B", PlayerRanking.LevelGroup.B},
                {"A", PlayerRanking.LevelGroup.A},
                {"M", PlayerRanking.LevelGroup.M},
                {"E", PlayerRanking.LevelGroup.E},
                {"E-M", PlayerRanking.LevelGroup.EM},
                {"M-A", PlayerRanking.LevelGroup.MA},
                {"A-B", PlayerRanking.LevelGroup.AB},
                {"B-C", PlayerRanking.LevelGroup.BC},
                {"C-D", PlayerRanking.LevelGroup.CD},
            };

            return levelDict[a.Split(' ').Skip(1).ToString()];
        }

        private void UpdateRankingsFromRow(PlayerRanking pr, int points, PlayerRanking.AgeGroup ageGroup, PlayerRanking.LevelGroup levelGroup, int category)
        {
            switch (category)
            {
                case (int)Rankings.Level:
                    pr.LevelPoints = points;
                    pr.Level = levelGroup;
                    break;
                case (int)Rankings.MS:
                    pr.SinglesPoints = points;
                    break;
                case (int)Rankings.WS:
                    pr.SinglesPoints = points;
                    break;
                case (int)Rankings.MD:
                    pr.DoublesPoints = points;
                    break;
                case (int)Rankings.WD:
                    pr.DoublesPoints = points;
                    break;
                case (int)Rankings.MXD:
                    pr.MixPoints = points;
                    break;
                case (int)Rankings.WXD:
                    pr.MixPoints = points;
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
            return int.Parse(elem.FindElement(By.ClassName("points")).GetAttribute("innerHTML"));
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

        private void WaitForPageLoad()
        {
            Thread.Sleep(3000);
        }
    }
}