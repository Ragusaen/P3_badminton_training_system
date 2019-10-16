using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    static class Constants
    {
        public const string RankingListElementClassName = "RankingListGrid";

        public enum EnumRankings {Level = 0, MS, WS, MD, WD, MXD, WXD}
        public static int RankingsCount = 7;
        public static string RankingRootUrl = "https://www.badmintonplayer.dk/DBF/Ranglister/#287,2019,,0,,,1492,0,,,,15,,,,0,,,,,,";
        public static string[] Categories = {"Level", "MS", "WS", "MD", "WD", "MXD", "WXD"};
    }
}
