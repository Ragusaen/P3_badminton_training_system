using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using System.Threading;
using System.IO;

namespace server.RankingsParser
{
    class Parser
    {
        public List<Dictionary<Player, PlayerRanking>> PlayerRankings;

        public void UpdateAllRankings(List<Player> players)
        {
            for (int i = 0; i < 1 /*Constants.RankingUrlArray.Length*/; i++)
            {
                string url = Constants.RankingUrlArray[i];

                string rawRanking = ScrapeRankingsWebsite(url);

                DistributeRankings(players, rawRanking);
            }
        }

        private string ScrapeRankingsWebsite(string url)
        {
            string result;
            var driver = new PhantomJSDriver();
            driver.Url = url;
            driver.Navigate();

            Thread.Sleep(2000);
            result = driver.FindElementByClassName("RankingListGrid").Text;

            File.WriteAllText(@"C:\users\odum\res.txt", result);

            return result;
        }

        private void DistributeRankings(List<Player> players, string rawRanking)
        { 
            string[] rankingArray = rawRanking.Split('\n');
            players.Sort((p1, p2) => p1.BadmintonId.CompareTo(p2.BadmintonId));

            // skips first row to skip the title
            for (int i = 1; i < rankingArray.Length; i++)
            {
                // players.BinarySearch();
            }
        }
    }
}
