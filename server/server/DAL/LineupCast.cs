using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    class LineUpCast {
        /// <summary>
        /// Creates the model version Lineup from the database positions
        /// </summary>
        public Lineup CreateLineup(ICollection<position> positionCollection)
        {
            // Create a new copy of list to avoid changing the underlying
            List<position> positions = positionCollection.ToList();

            Lineup lineup = new Lineup();

            // Iterate over all positions
            for (int i = 0; i < positions.Count; i++)
            {
                var dbPos = positions[i];

                // Check if the positions group (e.g. Mens Double, Womens Single) is already in the lineup, if not then create it
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

                // If the position type is a double position, then find the other player and add it to the lineup position.
                if (Lineup.PositionType.Double.HasFlag(((Lineup.PositionType)dbPos.Type)))
                {
                    var otherPosIndex = positions.FindIndex(s => s.Type == dbPos.Type && s.Order == dbPos.Order && s != dbPos);
                    var otherPlayer = positions[otherPosIndex];
                    newPosition.OtherPlayer = (Player) otherPlayer.member;
                    newPosition.OtherIsExtra = otherPlayer.IsExtra;

                    // Remove other player-position since it is now accounted for
                    positions.RemoveAt(otherPosIndex);
                }

                // Insert the new position into the correct groups positions
                var posList = lineup.Find(l => l.Type == (Lineup.PositionType) dbPos.Type).Positions;
                posList.Add(newPosition);
            }

            return lineup;
        }

        /// <summary>
        /// Creates the database positions from a model lineup
        /// </summary>
        /// <param name="db"> The database entity context to use</param>
        public ICollection<position> CreatePositions(Lineup lineup, teammatch match, DatabaseEntities db)
        {
            List<position> positions = new List<position>();

            // Iterate over all groups (e.g. Mens Double, Womens Single) in the lineup
            foreach (var group in lineup)
            {
                // Iterate over all positions in each group. (e.g. 1. Mens Single, 2. Mens Single)
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    // Add the player if one is set for this position
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

                    // If it is a double position, then also add the other player
                    if (Lineup.PositionType.Double.HasFlag(group.Type) && group.Positions[i].OtherPlayer != null)
                    {
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
            }

            return positions;
        }
    }
}
