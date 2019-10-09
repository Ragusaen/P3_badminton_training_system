using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    static class Constants
    {
        public enum EnumRankings { Level = 0, MS, WS, MD, WD, MMD, WMD }
        public enum EnumRankingsSimple { Level = 0, Singles, Doubles, MixedDoubles}

        public static string[] RankingUrlArray =
        {
            "https://www.badmintonplayer.dk/DBF/Ranglister/#287,2019,,0,,,1492,0,,,,15,,,,0,,,,,,",
            "https://www.badmintonplayer.dk/DBF/Ranglister/#288,2019,,0,,,1492,0,,,,15,,,,0,,,,,,M",
            "https://www.badmintonplayer.dk/DBF/Ranglister/#288,2019,,0,,,1492,0,,,,15,,,,0,,,,,,K",
            "https://www.badmintonplayer.dk/DBF/Ranglister/#289,2019,,0,,,1492,0,,,,15,,,,0,,,,,,M",
            "https://www.badmintonplayer.dk/DBF/Ranglister/#289,2019,,0,,,1492,0,,,,15,,,,0,,,,,,K",
            "https://www.badmintonplayer.dk/DBF/Ranglister/#292,2019,,0,,,1492,0,,,,15,,,,0,,,,,,M",
            "https://www.badmintonplayer.dk/DBF/Ranglister/#292,2019,,0,,,1492,0,,,,15,,,,0,,,,,,K"
        };
    }
}
