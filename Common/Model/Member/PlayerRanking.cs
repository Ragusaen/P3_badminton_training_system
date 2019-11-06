namespace Common.Model.Member
{
    class PlayerRanking
    {
        public string Level;

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
