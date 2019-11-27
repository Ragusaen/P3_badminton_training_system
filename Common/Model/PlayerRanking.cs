using System;

namespace Common.Model
{
    public class PlayerRanking
    {
        [Flags]
        public enum AgeGroup
        {
            Unknown, U09, U11, U13, U15, U17, U19, Senior
        }

        [Flags]
        public enum LevelGroup
        {
            Unknown, D, CD, C, BC, B, AB, A, MA, M, EM, E
        }

        public AgeGroup Age { get; set; }
        public LevelGroup Level { get; set; }
        public int LevelPoints { get; set; }
        public int SinglesPoints { get; set; }
        public int DoublesPoints { get; set; }
        public int MixPoints { get; set; }

        public override string ToString()
        {
            return $"LVL: {Age} {Level}, Level: {LevelPoints}, Singles: {SinglesPoints}, Doubles: {DoublesPoints} , Mixed: {MixPoints}";
        }
    }
}
