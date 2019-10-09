using NUnit.Framework;
using Server.Model;
using Server.RankingsParser;
using System.Collections.Generic;

namespace Test
{
    [TestFixture]
    [Parallelizable]
    public class ParserUpdatesLevelPointsForFirstPlayerOnRankings
    {
        [Test]
        public void Test()
        {
            List<Player> p = new List<Player>() { new Player(new Member()) };
            p[0].PlayerId = 96021601; p[0].Rankings = new PlayerRanking(); p[0].Rankings.LevelPoints = 50;
            Parser parser = new Parser();

            parser.UpdatePlayers(p);
            int actual = p[0].Rankings.LevelPoints;
            System.Console.WriteLine("Actual: "+actual);


            Assert.IsTrue(actual > 0);
        }
    }
    [TestFixture]
    [Parallelizable]
    public class ParserUpdatesLevelPointsForSecondPlayerOnRankings
    { 
    [Test]
        public void Test()
        {
            List<Player> p = new List<Player>() { new Player(new Member()), new Player(new Member()) };
            p[0].PlayerId = 96021601; p[0].Rankings = new PlayerRanking(); p[0].Rankings.LevelPoints = 50;
            p[1].PlayerId = 97022603; p[1].Rankings = new PlayerRanking(); p[1].Rankings.LevelPoints = 50;

            Parser parser = new Parser();

            parser.UpdatePlayers(p);
            int actual = p[1].Rankings.LevelPoints;
            System.Console.WriteLine("Actual: " + actual);


            Assert.IsTrue(actual > 0);
        }
    }
    [TestFixture]
    [Parallelizable]
    public class ParserUpdatesLevelPointsForSecondPlayerOnRankingsWhenFirstIsSkip
    {
        [Test]
        public void Test()
        {
            List<Player> p = new List<Player>() { new Player(new Member()), new Player(new Member()) };
            p[1].PlayerId = 97022603; p[1].Rankings = new PlayerRanking(); p[1].Rankings.LevelPoints = 50;

            Parser parser = new Parser();

            parser.UpdatePlayers(p);
            int actual = p[1].Rankings.LevelPoints;
            System.Console.WriteLine("Actual: " + actual);

            Assert.IsTrue(actual > 0);
        }
    }
    [TestFixture]
    [Parallelizable]
    public class ParserUpdatesSkillLevel
    {
        [Test]
        public void Test()
        {
            List<Player> p = new List<Player>() { new Player(new Member()), new Player(new Member()) };
            string expected = "SEN E-M";
            p[0].PlayerId = 96021601; p[0].Rankings = new PlayerRanking(); p[0].Rankings.LevelPoints = 50;

            Parser parser = new Parser();

            parser.UpdatePlayers(p);
            string actual = p[0].Rankings.SkillLevel;
            System.Console.WriteLine("Actual: " + actual);

            Assert.AreEqual(expected, actual);
        }
    }
    [TestFixture]
    [Parallelizable]
    public class ParserUpdatesSinglesPoints
    {
        [Test]
        public void Test()
        {
            List<Player> p = new List<Player>() { new Player(new Member()), new Player(new Member()) };
            p[0].PlayerId = 96021601; p[0].Rankings = new PlayerRanking(); p[0].Rankings.LevelPoints = 50;

            Parser parser = new Parser();

            parser.UpdatePlayers(p);
            int actual = p[0].Rankings.SinglesPoints;
            System.Console.WriteLine("Actual: " + actual);

            Assert.IsTrue(actual > 0);
        }
    }
}