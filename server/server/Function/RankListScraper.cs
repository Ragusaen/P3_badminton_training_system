using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Common.Model;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Server.DAL;

namespace Server.Function
{
    /// <summary>
    /// This class is used to scrape the rank list information from badmintonplayer.dk
    /// </summary>
    class RankListScraper
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public const string RankingListElementClassName = "RankingListGrid";

        // The ranklist categories, this uses bitflags
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

            // Start the chrome driver
            var browser = StartBrowser();
            NavigateCorrectVersion(browser);  

            // Set all the members in the database to not be on the ranklist, this will be changed
            // as they are found again
            foreach (var dbMember in _db.members)
            {
                dbMember.OnRankList = false;
            }

            // Start scraping from rank lists
            var players = new List<Player>();
            for (int i = 0; i < RankingsCount; i++)
            {
                _log.Debug("Scraping category: {category}", Categories[i]);
                
                // Find the category and click on it
                string nextCategoryXPath = $"/html/body/form/div[4]/div[1]/div[5]/div/div[{i + 1}]/a";
                browser.FindElement(By.XPath(nextCategoryXPath)).Click();
                WaitForPageLoad();

                // Scrape the raw data from this list
                List<IWebElement> rawRanking = ScrapeRankingsTable(browser);

                // Parse the data and assign it to the players
                DistributeRankings(players, rawRanking, 1 << i);

                // Try to scrape second page, throws exception if page doesn't exist
                try
                {
                    var nextPage = browser.FindElement(By.XPath("/html/body/form/div[4]/div[1]/div[5]/table/tbody/tr[102]/td/a"));
                    nextPage.Click();
                    WaitForPageLoad();
                    _log.Debug("Scraping page 2 for category: {category}", Categories[i]);

                    // Scrape the raw data from this list
                    rawRanking = ScrapeRankingsTable(browser);

                    // Parse the data and assign it to the players
                    DistributeRankings(players, rawRanking, 1 << i);
                }
                catch (Exception) { }
            }

            // Close the browser
            browser.Quit();
            browser.Dispose();

            // Write the players to the database
            UpdatePlayersInDatabase(players);
            _log.Debug("UpdatePlayers finished");
        }

        private void DistributeRankings(List<Player> players, List<IWebElement> rawRanking, int rankListIndex)
        {
            Category category = (Category) rankListIndex;

            // Go through all the rows from the raw ranking
            for (int j = 1; j < rawRanking.Count; j++) // Skips first row to avoid the title
            {
                // Try and find the player id, if not found skip this row
                var currentRow = rawRanking[j];
                try
                {
                    currentRow.FindElement(By.ClassName("playerid")).GetAttribute("innerHTML");
                }
                catch (Exception) { continue;}
                
                // Fetch information from the current row
                string rawPlayerId = currentRow.FindElement(By.ClassName("playerid")).GetAttribute("innerHTML");
                int badmintonPlayerId = RemoveFalseHyphen(rawPlayerId);
                int points = FetchPointsFromRow(currentRow);
                string ageAndLevel = FetchSkillLevelFromRow(currentRow);
                
                // The current player; will be set in if block below
                Player player;
                // If it is scraping the Level ranklist, find the player in the database or create a new one
                if (category == Category.Level)
                {
                    // Try finding the player in the database
                    member dbPlayer = _db.members.SingleOrDefault(p => p.BadmintonPlayerID == badmintonPlayerId);
                    if (dbPlayer != null) // If the player was found, add it
                    {
                        player = (Common.Model.Player)dbPlayer;
                        players.Add(player);
                    }
                    else // Create a new player and add them to the player list
                    {
                        string name = new string(currentRow.FindElement(By.ClassName("name")).Text.TakeWhile(p => p != ',').ToArray());

                        player = new Player
                        {
                            BadmintonPlayerId = badmintonPlayerId,
                            Rankings = new PlayerRanking(),
                            Member = new Member
                            {
                                Name = name,
                            }
                        };

                        _log.Debug($"New player found in Ranklist: {player.Member.Name} BadmintonId: {player.BadmintonPlayerId}");
                        players.Add(player);
                    }
                }
                else // If it is on sexed rank list
                {
                    player = players.SingleOrDefault(p => p.BadmintonPlayerId == badmintonPlayerId);
                    if (player != null) // If player has been found on the rank list
                    {
                        if (Category.Womens.HasFlag(category))
                            player.Sex = Sex.Female;
                        else if ((category & Category.Mens) != 0)
                            player.Sex = Sex.Male;
                        else
                            player.Sex = Sex.Unknown;
                    }
                    else // If the player wasn't found on the Level list, they are a mistake
                    {
                        continue;
                    }
                }
                        
                // Update the players rankings
                UpdateRankingsFromRow(player.Rankings, points, category);

                // If it is the level list then also add the age and level
                if (category == Category.Level)
                {
                    player.Rankings.Age = FetchAgeGroup(ageAndLevel);
                    player.Rankings.Level = FetchLevelGroup(ageAndLevel);
                }
            }
        }

        /// <summary>
        /// Updates the provided rankings based on the points and the category
        /// </summary>
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

        /// <summary>
        /// Updates the found players in the database
        /// </summary>
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


        /// <summary>
        /// Gets the skill level string from the given row
        /// </summary>
        private string FetchSkillLevelFromRow(IWebElement elem)
        {
            return elem.FindElement(By.ClassName("clas")).GetAttribute("innerHTML");
        }

        /// <summary>
        /// Gets the age group enum from a string
        /// </summary>
        private PlayerRanking.AgeGroup FetchAgeGroup(string ageGroupString)
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
            string res = ageGroupString.Split(' ').FirstOrDefault();

            if (res == null)
                return PlayerRanking.AgeGroup.Unknown;

            return ageDict[res]; 
        }

        private PlayerRanking.LevelGroup FetchLevelGroup(string levelGroupString)
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
            string res = levelGroupString.Split(' ').LastOrDefault();

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

        /// <summary>
        /// Remove weird hyphen symbol and convert to int
        /// </summary>
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

        private IWebDriver StartBrowser()
        { 
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless");
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            IWebDriver browser = new ChromeDriver(chromeOptions);

            // Navigate to the clubs rankings
            browser.Navigate().GoToUrl(RankingRootUrl);
            WaitForPageLoad();

            return browser;
        }

        private void NavigateCorrectVersion(IWebDriver browser)
        {
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