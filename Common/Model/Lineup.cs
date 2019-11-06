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

        public Match Match { get; set; }
        public Dictionary<Tuple<PositionType, int>, Position> Positions { get; set; }
    }
}

