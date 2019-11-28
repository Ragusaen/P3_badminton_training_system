using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
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
