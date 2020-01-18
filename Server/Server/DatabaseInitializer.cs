using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using server.DAL;

namespace server
{
    public class DatabaseInitializer
    {
        private DatabaseEntities _db = new DatabaseEntities();

        public void Initialize()
        {
            {
                var headCoach = _db.members.Single(p => p.BadmintonPlayerID == 95032401);
                var secondCoach = _db.members.Single(p => p.BadmintonPlayerID == 81100101);
                headCoach.MemberType = (int) MemberType.Both;
                secondCoach.MemberType = (int) MemberType.Both;
            }

            var fp1 = new focuspoint
            {
                Description =
                    "This focus point should be trained during individual time as it is a technical exercise. The exercise includes 1 - 2 pushes and then a lift to the backhand, repeat.",
                IsPrivate = false,
                Name = "Defensive long backhand",
                VideoURL = "https://www.youtube.com/watch?v=fzmT3qcMq4s",
            };

            var fp2 = new focuspoint
            {
                Description =
                    "Stay uncomfortably close to the net. The supporting player makes a not too high lift, the exercisor will attempt to attack on the lift. Lifts can be in both sides.",
                IsPrivate = false,
                Name = "Fast movement to the rear court",
                VideoURL = null,
            };

            var fp3 = new focuspoint
            {
                Description = "This focus point should be trained during matches.",
                IsPrivate = false,
                Name = "Forcing net play in doubles",
                VideoURL = null,
            };

            var fp4 = new focuspoint
            {
                Description = "This focus point should be trained during matches.",
                IsPrivate = false,
                Name = "Defensive close to line.",
                VideoURL = null,
            };
            _db.focuspoints.AddRange(new focuspoint[] {fp1, fp2, fp3, fp4});

            var tuesdayPrac = _db.practiceteams.Add(new practiceteam
            {
                Name = "Tuesday Practice",
                trainer = headCoach,
            });
            var wednesdayPrac = _db.practiceteams.Add(new practiceteam
            {
                Name = "Wednesday Practice",
                trainer = headCoach,
            });
            var thursdayPrac = _db.practiceteams.Add(new practiceteam
            {
                Name = "Thursday Practice",
                trainer = secondCoach,
            });
            tuesdayPrac.players = _db.members.Where(p => p.ID < 31 || (p.Sex == (int)Sex.Female && p.ID < 46)).ToList();
            thursdayPrac.players = tuesdayPrac.players;
            wednesdayPrac.players = _db.members.Where(p => p.ID > 14 && p.ID < 27).ToList();

            var tuesdaySession = _db.practicesessions.Add(new practicesession
            {
                mainfocuspoint = fp2,
                playsession = new playsession()
                {
                    EndDate = new DateTime(2020, 1, 21, 21, 30, 0),
                    StartDate = new DateTime(2020, 1, 21, 19, 30, 0),
                    Location = "Vester Mariendal Skole",
                    Type = (int)PlaySession.Type.Practice
                },
                practiceteam = tuesdayPrac,
                trainer = headCoach,
            });
            
            var wednesdaySession = _db.practicesessions.Add(new practicesession
            {
                mainfocuspoint = fp2,
                playsession = new playsession()
                {
                    EndDate = new DateTime(2020, 1, 22, 19, 0, 0),
                    StartDate = new DateTime(2020, 1, 22, 17, 0, 0),
                    Location = "Vester Mariendal Skole",
                    Type = (int)PlaySession.Type.Practice
                },
                practiceteam = wednesdayPrac,
                trainer = headCoach,
            });
            
            var thusdaySession = _db.practicesessions.Add(new practicesession
            {
                mainfocuspoint = fp2,
                playsession = new playsession()
                {
                    EndDate = new DateTime(2020, 1, 23, 21, 0, 0),
                    StartDate = new DateTime(2020, 1, 23, 19, 0, 0),
                    Location = "Vester Mariendal Skole",
                    Type = (int)PlaySession.Type.Practice
                },
                practiceteam = thursdayPrac,
                trainer = secondCoach,
            });
            
            var firstTeamPlayers = _db.members.Where(p => (new int[] { 1, 2, 3, 4, 5, 21, 29, 33 }).Contains(p.ID)).ToList();
            var tritonVsGreve2 = _db.teammatches.Add(new teammatch
            {
                playsession = new playsession()
                {
                    EndDate = new DateTime(2020,1,25,18,0,0),
                    StartDate = new DateTime(2020, 1, 25, 15, 0, 0),
                    Location = "Vester Mariendal Skole",
                    Type = (int)PlaySession.Type.Match
                },
                LeagueRound = 6,
                captain = headCoach,
                League = (int)TeamMatch.Leagues.Division1,
                OpponentName = "Greve 2",
                Season = 2019,
                TeamIndex = 1,
            });
            
            var tritonVsGug = _db.teammatches.Add(new teammatch
            {
                playsession = new playsession()
                {
                    EndDate = new DateTime(2020, 1, 11, 17, 0, 0),
                    StartDate = new DateTime(2020, 1, 11, 14, 0, 0),
                    Location = "Vester Mariendal Skole 1-3",
                    Type = (int)PlaySession.Type.Match
                },
                LeagueRound = 7,
                captain = secondCoach,
                League = (int)TeamMatch.Leagues.Division3,
                OpponentName = "Gug",
                Season = 2019,
                TeamIndex = 2,
            });
            
            tritonVsGreve2.positions = new List<position>
            {
                new position
                {
                    member = _db.members.Find(1),
                    IsExtra = false,
                    Order = 0,
                    Type = (int)Lineup.PositionType.MixDouble,
                },
                new position
                {
                    member = _db.members.Find(21),
                    IsExtra = false,
                    Order = 0,
                    Type = (int)Lineup.PositionType.MixDouble,
                },
                new position
                {
                    member = _db.members.Find(4),
                    IsExtra = false,
                    Order = 1,
                    Type = (int)Lineup.PositionType.MixDouble,
                },
                new position
                {
                    member = _db.members.Find(33),
                    IsExtra = false,
                    Order = 1,
                    Type = (int)Lineup.PositionType.MixDouble,
                },
                new position
                {
                    member = _db.members.Find(21),
                    IsExtra = false,
                    Order = 0,
                    Type = (int)Lineup.PositionType.WomensSingle,
                },
                new position
                {
                    member = _db.members.Find(39),
                    IsExtra = false,
                    Order = 1,
                    Type = (int)Lineup.PositionType.WomensSingle,
                },
                new position
                {
                    member = _db.members.Find(5),
                    IsExtra = false,
                    Order = 0,
                    Type = (int)Lineup.PositionType.MensSingle,
                },
                new position
                {
                    member = _db.members.Find(6),
                    IsExtra = false,
                    Order = 1,
                    Type = (int)Lineup.PositionType.MensSingle,
                },
                new position
                {
                    member = _db.members.Find(33),
                    IsExtra = false,
                    Order = 0,
                    Type = (int)Lineup.PositionType.WomensDouble,
                },
                new position
                {
                    member = _db.members.Find(39),
                    IsExtra = false,
                    Order = 0,
                    Type = (int)Lineup.PositionType.WomensDouble,
                },
                new position
                {
                    member = _db.members.Find(1),
                    IsExtra = false,
                    Order = 0,
                    Type = (int)Lineup.PositionType.MensDouble,
                },
                new position
                {
                    member = _db.members.Find(4),
                    IsExtra = false,
                    Order = 0,
                    Type = (int)Lineup.PositionType.MensDouble,
                },
                new position
                {
                    member = _db.members.Find(5),
                    IsExtra = false,
                    Order = 1,
                    Type = (int)Lineup.PositionType.MensDouble,
                },
                new position
                {
                    member = _db.members.Find(6),
                    IsExtra = false,
                    Order = 1,
                    Type = (int)Lineup.PositionType.MensDouble,
                },
            };
            
            _db.SaveChanges();
        }
    }
}
