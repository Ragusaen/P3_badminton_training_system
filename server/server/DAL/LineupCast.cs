using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    class LineUpCast {
        public Lineup CreateLineup(ICollection<position> positionCollection)
        {
            List<position> positions = positionCollection.ToList();

            Lineup lineup = new Lineup();

            foreach (position dbPos in positions)
            {
                if (lineup.All(l => l.type != (Lineup.PositionType) dbPos.Type))
                    lineup.Add( ((Lineup.PositionType)dbPos.Type, new List<Position>()) );

                var newPosition = new Position()
                {
                    Player = (Player)dbPos.member,
                    IsExtra = dbPos.IsExtra
                };


                if (Lineup.PositionType.Double.HasFlag(((Lineup.PositionType)dbPos.Type)))
                {
                    var otherPos = positions.Find(s => s.Type == dbPos.Type && s.Order == dbPos.Order && s != dbPos);
                    newPosition.OtherPlayer = (Player) otherPos.member;
                    newPosition.OtherIsExtra = otherPos.IsExtra;
                }

                var posList = lineup.Find(l => l.type == (Lineup.PositionType) dbPos.Type).positions;
                posList.Add(newPosition);
            }

            return lineup;
        }
    }
}
