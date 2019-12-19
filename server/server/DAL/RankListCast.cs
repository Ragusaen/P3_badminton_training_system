using Common.Model;

namespace server.DAL
{
    partial class ranklist
    {
        public static explicit operator PlayerRanking(ranklist r)
        {
            return new PlayerRanking
            {
                SinglesPoints = r.SinglesPoints,
                DoublesPoints = r.DoublesPoints,
                MixPoints = r.MixPoints,
                Level = (PlayerRanking.LevelGroup) r.Level,
                LevelPoints = r.LevelPoints,
                Age = (PlayerRanking.AgeGroup)r.AgeGroup
            };
        }
    }
}
