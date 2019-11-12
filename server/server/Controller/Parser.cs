﻿using System;
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

        public void UpdatePlayers()
        {
            _log.Debug("UpdatePlayers startup");
            List<Player> players = null;// pdao.ReadAll().ToList();

            var chromeOptions = new ChromeOptions();
            // chromeOptions.AddArguments("--headless"); 
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            IWebDriver browser = new ChromeDriver(chromeOptions);

            FindRootRankList(browser);

            for (int i = 0; i < Constants.RankingsCount; i++)
            {
                _log.Debug("Scraping category: {category}", Constants.Categories[i]);
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

                    _log.Debug("Scraping page 2 for category: {category}", Constants.Categories[i]);
                    rawRanking = ScrapeRankingsTable(browser);
                    DistributeRankings(players, rawRanking, i);
                }
                catch (Exception) { }
            }
            browser.Quit();

            //pdao.WriteMany(players);
        }

        private void FindRootRankList(IWebDriver browser)
        {
            browser.Navigate().GoToUrl(Constants.RankingRootUrl);
            WaitForPageLoad();

            string xpath = null;
            bool CorrectVersion = false;
            int i = 0;
            while (!CorrectVersion)
            {
                xpath = $"/html/body/form/div[4]/div[1]/div[2]/div[6]/select/option[{++i}]";
                CorrectVersion = browser.FindElement(By.XPath(xpath)).Text.Contains("senior");
            }

            _log.Debug("Using rank list version: {0}", browser.FindElement(By.XPath(xpath)).Text);
            browser.FindElement(By.XPath(xpath)).Click();

            browser.FindElement(By.Id("LinkButtonSearch")).Click();
            WaitForPageLoad();
        }

        private List<IWebElement> ScrapeRankingsTable(IWebDriver driver)
        {
            WaitForPageLoad();
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
                    UpdateRankingsFromRow(p2.Rankings, points, level, category);
                }
                else
                {
                    string name = new string(currentRow.FindElement(By.ClassName("name")).Text.TakeWhile(p => p != ',').ToArray());
                    int sex = 0;
                    var playerRanking = new PlayerRanking();
                    UpdateRankingsFromRow(playerRanking, points, level, category);
                    
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

        private void UpdateRankingsFromRow(PlayerRanking pr, int points, string level, int category)
        {
            switch (category)
            {
                case (int)Constants.EnumRankings.Level:
                    pr.LevelPoints = points;
                    pr.Level = level;
                    break;
                case (int)Constants.EnumRankings.MS:
                    pr.SinglesPoints = points;
                    break;
                case (int)Constants.EnumRankings.WS:
                    pr.SinglesPoints = points;
                    break;
                case (int)Constants.EnumRankings.MD:
                    pr.DoublesPoints = points;
                    break;
                case (int)Constants.EnumRankings.WD:
                    pr.DoublesPoints = points;
                    break;
                case (int)Constants.EnumRankings.MXD:
                    pr.MixPoints = points;
                    break;
                case (int)Constants.EnumRankings.WXD:
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

        private void WaitForPageLoad()
        {
            Thread.Sleep(3000);
        }
    }
}