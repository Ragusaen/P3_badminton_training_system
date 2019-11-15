namespace Common.Model
{
    public class PlayerRanking
    {
        public enum AgeGroup
        {
            Unknown, U09, U11, U13, U15, U17, U19, Senior
        }

        public enum LevelGroup
        {
            Unknown, D, CD, C, BC, B, AB, A, MA, M, EM, E
        }

        public AgeGroup Age;
        public LevelGroup Level;
        public int LevelPoints;
        public int SinglesPoints;
        public int DoublesPoints;
        public int MixPoints;

        public override string ToString()
        {
            return $"LVL: {Age} {Level}, Level: {LevelPoints}, Singles: {SinglesPoints}, Doubles: {DoublesPoints} , Mixed: {MixPoints}";
        }
    }
}
