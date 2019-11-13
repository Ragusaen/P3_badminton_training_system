namespace Common.Model
{
    public class PlayerRanking
    {
        public enum AgeGroup
        {
            U9,
            U11,
            U13,
            U15,
            U17,
            Senior,
            Veteran
        }

        public int Level;

        public AgeGroup Age;
        public int LevelPoints;
        public int SinglesPoints;
        public int DoublesPoints;
        public int MixPoints;

        public override string ToString()
        {
            return $"LVL: {Level}, Level: {LevelPoints}, Singles: {SinglesPoints}, Doubles: {DoublesPoints} , Mixed: {MixPoints}";
        }
    }
}
