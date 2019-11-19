using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Threading;
using Common.Model;
using NLog;
using Server.DAL;

namespace Server.Controller
{
    class RankListScraper
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public const string RankingListElementClassName = "RankingListGrid";

        [Flags]
        public enum Category { 
            Level = 1, 
            MS = 2, 
            WS = 4, 
            MD = 8, 
            WD = 16, 
            MXD = 32, 
            WXD = 64,
            Mens = MS | MD | MXD,
            Womens = WS | WD | WXD,
        }
        public static int RankingsCount = 7;
        public static string RankingRootUrl = "https://www.badmintonplayer.dk/DBF/Ranglister/#287,2019,,0,,,1492,0,,,,15,,,,0,,,,,,";
        public static string[] Categories = { "Level", "MS", "WS", "MD", "WD", "MXD", "WXD" };

        DatabaseEntities _db = new DatabaseEntities();

        public void UpdatePlayers()
        {
            _log.Debug("UpdatePlayers started");
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless"); 
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            IWebDriver browser = new ChromeDriver(chromeOptions);
            FindRootRankList(browser);

            foreach (var dbMember in _db.members)
            {
                dbMember.OnRankList = false;
            }

            var players = new List<Player>();
            for (int i = 0; i < RankingsCount; i++)
            {
                _log.Debug("Scraping category: {category}", Categories[i]);
                string nextCategoryXPath = $"/html/body/form/div[4]/div[1]/div[5]/div/div[{i + 1}]/a";
                browser.FindElement(By.XPath(nextCategoryXPath)).Click();
                WaitForPageLoad();

                List<IWebElement> rawRanking = ScrapeRankingsTable(browser);

                DistributeRankings(players, rawRanking, 1 << i);

                try // Checking for next page
                {
                    var nextPage = browser.FindElement(By.XPath("/html/body/form/div[4]/div[1]/div[5]/table/tbody/tr[102]/td/a"));
                    nextPage.Click();
                    WaitForPageLoad();
                    _log.Debug("Scraping page 2 for category: {category}", Categories[i]);

                    rawRanking = ScrapeRankingsTable(browser);
                    DistributeRankings(players, rawRanking, 1 << i);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch (Exception) { }
            }
            browser.Quit();

            UpdatePlayersInDatabase(players);
            _log.Debug("UpdatePlayers finished");
        }

        private void DistributeRankings(List<Player> players, List<IWebElement> rawRanking, int i)
        {
            Category category = (Category) i;

            // skips first row to avoid the title
            for (int j = 1; j < rawRanking.Count; j++)
            {
                var currentRow = rawRanking[j];
                try
                {
                    currentRow.FindElement(By.ClassName("playerid")).GetAttribute("innerHTML");
                }
                catch (Exception) { continue;}
                
                // Fetches information from the current row
                string rawPlayerId = currentRow.FindElement(By.ClassName("playerid")).GetAttribute("innerHTML");
                int badmintonPlayerId = RemoveFalseHyphen(rawPlayerId);
                int points = FetchPointsFromRow(currentRow);
                string ageAndLevel = FetchSkillLevelFromRow(currentRow);

                Server.DAL.member dbPlayer = _db.members.SingleOrDefault(p => p.BadmintonPlayerID == badmintonPlayerId);
                Player player;
                if (players.Exists(p => p.BadmintonPlayerId == badmintonPlayerId))
                {
                    player = players.Single(p => p.BadmintonPlayerId == badmintonPlayerId);

                    if ((category & Category.Womens) > 0)
                        player.Sex = Sex.Female;
                    else if((category & Category.Mens) > 0)
                        player.Sex = Sex.Male;
                    else
                        player.Sex = Sex.Unknown;
                }
                else if (dbPlayer != null)
                {
                    player = (Common.Model.Player)dbPlayer;
                    players.Add(player);
                }
                else
                {
                    string name = new string(currentRow.FindElement(By.ClassName("name")).Text.TakeWhile(p => p != ',').ToArray());
                    var playerRanking = new PlayerRanking();

                    player = new Player
                    {
                        BadmintonPlayerId = badmintonPlayerId,
                        Rankings = playerRanking,
                        Member = new Member
                        {
                            Name = name,
                        }
                    };

                    _log.Debug($"New player found in Ranklist: {player.Member.Name} BadmintonId: {player.BadmintonPlayerId}");
                    players.Add(player);
                }

                UpdateRankingsFromRow(player.Rankings, points, category);
                if (category == Category.Level)
                {
                    player.Rankings.Age = FetchAgeGroup(ageAndLevel);
                    player.Rankings.Level = FetchLevelGroup(ageAndLevel);
                }
            }
        }

        private void UpdateRankingsFromRow(PlayerRanking pr, int points, Category category)
        {
            switch (category)
            {
                case Category.Level:
                    pr.LevelPoints = points;
                    break;
                case Category.MS:
                case Category.WS:
                    pr.SinglesPoints = points;
                    break;
                case Category.MD:
                case Category.WD:
                    pr.DoublesPoints = points;
                    break;
                case Category.MXD:
                case Category.WXD:
                    pr.MixPoints = points;
                    break;
                default:
                    throw new ArgumentException($"Category could not be recognised. Category is: {category}");
            }
        }

        private void UpdatePlayersInDatabase(List<Player> players)
        {
            foreach (var p in players)
            {
                Server.DAL.member dbMember;
                ranklist dbRankList;

                if (p.Member.Id > 0)
                {
                    dbMember = _db.members.Single(p2 => p2.ID == p.Member.Id);
                    dbRankList = _db.ranklists.Single(p2 => p2.MemberID == p.Member.Id);
                }
                else
                {
                    dbMember = _db.members.Add(new Server.DAL.member());
                    dbRankList = dbMember.ranklist = new ranklist();
                    dbMember.BadmintonPlayerID = p.BadmintonPlayerId;
                    dbMember.MemberType = (int)MemberType.Player;
                    dbMember.Name = p.Member.Name;
                }

                dbMember.OnRankList = true;
                dbMember.Sex = (int)p.Sex;
                dbRankList.AgeGroup = (int)p.Rankings.Age;
                dbRankList.Level = (int)p.Rankings.Level;
                dbRankList.DoublesPoints = p.Rankings.DoublesPoints;
                dbRankList.LevelPoints = p.Rankings.LevelPoints;
                dbRankList.MixPoints = p.Rankings.MixPoints;
                dbRankList.SinglesPoints = p.Rankings.SinglesPoints;
            }
            _db.SaveChanges();
        }

        private string FetchSkillLevelFromRow(IWebElement elem)
        {
            return elem.FindElement(By.ClassName("clas")).GetAttribute("innerHTML");
        }

        private PlayerRanking.AgeGroup FetchAgeGroup(string a)
        {
            var ageDict = new Dictionary<string, PlayerRanking.AgeGroup>
            {
                {"U09", PlayerRanking.AgeGroup.U09},
                {"U11", PlayerRanking.AgeGroup.U11},
                {"U13", PlayerRanking.AgeGroup.U13},
                {"U15", PlayerRanking.AgeGroup.U15},
                {"U17", PlayerRanking.AgeGroup.U17},
                {"U19", PlayerRanking.AgeGroup.U19},
                {"SEN", PlayerRanking.AgeGroup.Senior}
            };
            string res = a.Split(' ').FirstOrDefault();

            if (res == null)
                return PlayerRanking.AgeGroup.Unknown;

            return ageDict[res]; 
        }

        private PlayerRanking.LevelGroup FetchLevelGroup(string a)
        {
            var levelDict = new Dictionary<string, PlayerRanking.LevelGroup>
            {
                {"D", PlayerRanking.LevelGroup.D},
                {"C-D", PlayerRanking.LevelGroup.CD},
                {"C", PlayerRanking.LevelGroup.C},
                {"B-C", PlayerRanking.LevelGroup.BC},
                {"B", PlayerRanking.LevelGroup.B},
                {"A-B", PlayerRanking.LevelGroup.AB},
                {"A", PlayerRanking.LevelGroup.A},
                {"M-A", PlayerRanking.LevelGroup.MA},
                {"M", PlayerRanking.LevelGroup.M},
                {"E-M", PlayerRanking.LevelGroup.EM},
                {"E", PlayerRanking.LevelGroup.E},
            };
            string res = a.Split(' ').LastOrDefault();

            if (res == null)
                return PlayerRanking.LevelGroup.Unknown;

            return levelDict[res]; 
        }

        private List<IWebElement> ScrapeRankingsTable(IWebDriver driver)
        {
            WaitForPageLoad();
            return driver.FindElement(By.ClassName(RankingListElementClassName)).FindElements(By.TagName("tr")).ToList();
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

        private void WaitForPageLoad()
        {
            Thread.Sleep(3000);
        }
    }
}