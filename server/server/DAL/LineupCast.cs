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

            for (int i = 0; i < positions.Count; i++)
            {
                var dbPos = positions[i];
                if (lineup.All(l => l.Type != (Lineup.PositionType) dbPos.Type))
                    lineup.Add( new Lineup.Group() {
                        Type = (Lineup.PositionType)dbPos.Type,
                        Positions = new List<Position>()
                        }
                    );

                var newPosition = new Position()
                {
                    Player = (Player)dbPos.member,
                    IsExtra = dbPos.IsExtra
                };


                if (Lineup.PositionType.Double.HasFlag(((Lineup.PositionType)dbPos.Type)))
                {
                    var otherPosIndex = positions.FindIndex(s => s.Type == dbPos.Type && s.Order == dbPos.Order && s != dbPos);
                    var otherPlayer = positions[otherPosIndex];
                    newPosition.OtherPlayer = (Player) otherPlayer.member;
                    newPosition.OtherIsExtra = otherPlayer.IsExtra;

                    positions.RemoveAt(otherPosIndex);
                }

                var posList = lineup.Find(l => l.Type == (Lineup.PositionType) dbPos.Type).Positions;
                posList.Add(newPosition);
            }

            return lineup;
        }

        public ICollection<position> CreatePositions(Lineup lineup, teammatch match, DatabaseEntities db)
        {
            List<position> positions = new List<position>();

            foreach (var group in lineup)
            {
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    if (group.Positions[i].Player != null)
                    {
                        positions.Add(new position()
                        {
                            IsExtra = group.Positions[i].IsExtra,
                            member = db.members.Find(group.Positions[i].Player.Member.Id),
                            Order = i,
                            Type = (int)group.Type,
                            teammatch = match
                        });
                    }

                    if (Lineup.PositionType.Double.HasFlag(group.Type) && group.Positions[i].OtherPlayer != null)
                        positions.Add(new position()
                        {
                            IsExtra = group.Positions[i].OtherIsExtra,
                            member = db.members.Find(group.Positions[i].OtherPlayer.Member.Id),
                            Order = i, 
                            Type = (int)group.Type,
                            teammatch = match
                        });
                }
            }

            return positions;
        }
    }
}
