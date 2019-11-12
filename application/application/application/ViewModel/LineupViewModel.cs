using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace application.ViewModel
{
    class LineupViewModel : BaseViewModel
    {
        private Dictionary<Tuple<Lineup.PositionType, int>, Position> _positions;

        public Dictionary<Tuple<Lineup.PositionType, int>, Position> Positions
        {
            get { return _positions; }
            set { SetProperty(ref _positions, value); }
        }


        public LineupViewModel()
        {
            Positions = new Dictionary<Tuple<Lineup.PositionType, int>, Position>();
            //Positions.Add(new Tuple<Lineup.PositionType, int>(Lineup.PositionType.MensSingle, 4), new Player() { BadmintonPlayerId = 123 });
            //Positions.Add(new Tuple<Lineup.PositionType, int>(Lineup.PositionType.MixDouble, 3), new Player() { BadmintonPlayerId = 321 });

            Positions.Add(new Tuple<Lineup.PositionType, int>(Lineup.PositionType.MixDouble, 0), new Position() { Player = new Player() { BadmintonPlayerId = 1234 } });
            Positions.Add(new Tuple<Lineup.PositionType, int>(Lineup.PositionType.MixDouble, 1), new Position() { Player = new Player() { BadmintonPlayerId=1234} });
        }
    }
}
