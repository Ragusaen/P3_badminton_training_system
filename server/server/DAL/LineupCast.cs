using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    class LineUpCast {
        public Lineup CreateLineup(ICollection<position> p)
        {
            List<position> positions = p.ToList();

            Lineup lineup = new Lineup()
            {
                Positions = new Dictionary<Tuple<Lineup.PositionType, int>, Position>()
            };

            foreach (position dbPos in positions)
            {

                Position dicPos = new Position()
                {
                    Player = (Player)dbPos.member,
                    IsExtra = dbPos.IsExtra != 0
                };
                if (Lineup.IsDoublePosition((Lineup.PositionType) dbPos.Type))
                {
                    int index =
                        positions.FindIndex(u => u.Type == dbPos.Type && u.Order == dbPos.Order && u != dbPos);
                    position otherDbPos = positions[index];
                    positions.RemoveAt(index);

                    dicPos.OtherPlayer = (Player) otherDbPos.member;
                    dicPos.OtherIsExtra = otherDbPos.IsExtra != 0;
                }
                lineup.Positions.Add(
                    new Tuple<Lineup.PositionType, int>((Lineup.PositionType)dbPos.Type, dbPos.Order),
                    dicPos
                    );
            }

            return lineup;
        }
    }
}
