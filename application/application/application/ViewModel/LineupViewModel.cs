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

            Position p = new Position();
            p.Player = new Player() { BadmintonPlayerId = 1234, Member = new Member() { Name = "Bob" } };
            p.OtherPlayer = new Player() { BadmintonPlayerId = 4321, Member = new Member() { Name = "Jens" } };

            Position q = new Position();
            q.Player = new Player() { BadmintonPlayerId = 1234, Member = new Member() { Name = "Jens" } };

            Positions.Add(new Tuple<Lineup.PositionType, int>(Lineup.PositionType.MensDouble, 1), p);
            Positions.Add(new Tuple<Lineup.PositionType, int>(Lineup.PositionType.MensSingle, 2), q);
        }
    }
}
