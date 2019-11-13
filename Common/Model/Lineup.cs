using System;
using System.Collections.Generic;

namespace Common.Model
{
    public class Lineup
    {
        public enum PositionType
        {
            MensSingle,
            WomensSingle,
            MensDouble,
            WomensDouble,
            MixDouble
        }

        public static bool IsDoublePosition(PositionType type)
        {
            switch (type)
            {
                case PositionType.MensDouble:
                case PositionType.WomensDouble:
                case PositionType.MixDouble:
                    return true;
                default:
                    return false;
            }
        }

        public Dictionary<Tuple<PositionType, int>, Position> Positions { get; set; }
    }
}