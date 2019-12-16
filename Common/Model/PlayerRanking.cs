using System;

namespace Common.Model
{
    public class PlayerRanking
    {
        [Flags]
        public enum AgeGroup
        {
            Unknown = 0, U09 = 1, U11 = 2, U13 = 4, U15 = 8, U17 = 16, U19 = 32, Senior = 64
        }

        [Flags]
        public enum LevelGroup
        {
            Unknown = 0, D = 1, C = 2, B = 4, A = 8, M = 16, E = 32,
            CD = D | C, BC = C | B, AB = B | A, MA = A | M, EM = M | E,
        }

        public AgeGroup Age { get; set; }
        public LevelGroup Level { get; set; }
        public int LevelPoints { get; set; }
        public int SinglesPoints { get; set; }
        public int DoublesPoints { get; set; }
        public int MixPoints { get; set; }
    }
}
