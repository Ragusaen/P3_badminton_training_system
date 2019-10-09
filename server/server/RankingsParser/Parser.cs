using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace Server.RankingsParser
{
    class Parser
    {
        private const string RankingListElementClassName = "RankingListGrid";

        public void UpdatePlayers(List<Player> players)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless");
            IWebDriver browser = new ChromeDriver(chromeOptions);

            for (int i = 0; i < Constants.RankingUrlArray.Length; i++)
            {
                browser.Navigate().GoToUrl(Constants.RankingUrlArray[i]);

                List<IWebElement> rawRanking = ScrapeRankingsWebsite(browser);

                DistributeRankings(players, rawRanking, i);
                Console.WriteLine("Completed: " + i);
            }
        }

        private List<IWebElement> ScrapeRankingsWebsite(IWebDriver driver)
        {
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(3000));

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div[4]/div[1]/div[5]/table")));
            Thread.Sleep(1000);

            return driver.FindElement(By.ClassName(RankingListElementClassName)).FindElements(By.TagName("tr")).ToList();
        }

        private void DistributeRankings(List<Player> players, List<IWebElement> rawRanking, int category)
        { 
            // skips first row to avoid the title
            for (int i = 1; i < rawRanking.Count-1; i++)
            {
                IWebElement elem = rawRanking[i];
                string rawPlayerid = rawRanking[i].FindElement(By.ClassName("playerid")).GetAttribute("innerHTML");
                int playerid = RemoveFalseHyphen(rawPlayerid);

                bool playerExists = players.Exists(p => p.PlayerId.Equals(playerid));

                if (playerExists)
                {
                    Player a = players.Find(p => p.PlayerId.Equals(playerid));
                    a.Rankings.SkillLevel = FetchSkillLevelFromRow(elem);

                    switch (category)
                    {
                        case (int)Constants.EnumRankings.Level:
                            a.Rankings.LevelPoints = FetchPointsFromRow(elem);
                            break;
                        case (int)Constants.EnumRankings.MS:
                            a.Rankings.SinglesPoints = FetchPointsFromRow(elem);
                            break;
                        case (int)Constants.EnumRankings.WS:
                            a.Rankings.SinglesPoints = FetchPointsFromRow(elem);
                            break;
                        case (int)Constants.EnumRankings.MD:
                            a.Rankings.DoublesPoints = FetchPointsFromRow(elem);
                            break;
                        case (int)Constants.EnumRankings.WD:
                            a.Rankings.DoublesPoints = FetchPointsFromRow(elem);
                            break;
                        case (int)Constants.EnumRankings.MMD:
                            a.Rankings.MixedDoublesPoints = FetchPointsFromRow(elem);
                            break;
                        case (int)Constants.EnumRankings.WMD:
                            a.Rankings.MixedDoublesPoints = FetchPointsFromRow(elem);
                            break;
                        default:
                            break;
                    }
                }
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
            byte[] newBytes = new byte[bytes.Length-3];

            int i = 0;

            while (!(bytes[i] == 0xE2 && bytes[i + 1] == 0x80 && bytes[i + 2] == 0x91) && i < bytes.Length) // while(byte sequence does not equal the Non-Breaking-Hyphen) { ... }
                i++;
            Array.Copy(bytes, 0, newBytes, 0, i);
            Array.Copy(bytes, i + 3, newBytes, i, 2);

            return Int32.Parse(Encoding.UTF8.GetString(newBytes));
        }
    }
}
