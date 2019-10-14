using NUnit.Framework;
using Server.Model;
using Server.Controller;
using System.Collections.Generic;

namespace RankingsParserTest.UpdatePlayers
{
    [TestFixture]
    [Parallelizable]
    public class ParserUpdatesLevelPointsForFirstPlayerOnRankings
    {
        [Test]
        public void Test()
        {
            List<Player> p = new List<Player>() { new Player(new Member(), 96021601) };
            p[0].Rankings = new PlayerRanking(); 
            Parser parser = new Parser();

            parser.UpdatePlayers();
            int actual = p[0].Rankings.LevelPoints;
            System.Console.WriteLine("Actual: " + actual);



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

            parser.UpdatePlayers();
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
            p[1].BadmintonPlayerId = 97022603; p[1].Rankings = new PlayerRanking(); 

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
            p[0].BadmintonPlayerId = 96021601; p[0].Rankings = new PlayerRanking(); 

            Parser parser = new Parser();

            parser.UpdatePlayers(p);
            string actual = p[0].Rankings.Level;
            System.Console.WriteLine("Actual: " + actual);

            Assert.AreEqual(expected, actual);
        }
    }
    [TestFixture]
    [Parallelizable]
    public class ParserUpdatesMensSinglesPoints
    {
        [Test]
        public void Test()
        {
            List<Player> p = new List<Player>() { new Player(new Member()), new Player(new Member()) };
            p[0].BadmintonPlayerId = 97022603; p[0].Rankings = new PlayerRanking(); 

            Parser parser = new Parser();

            parser.UpdatePlayers(p);
            int actual = p[0].Rankings.SinglesPoints;
            System.Console.WriteLine("Actual: " + actual);

            Assert.AreNotEqual(0, actual);
        }
    }
    [TestFixture]
    [Parallelizable]
    public class ParserUpdatesWomensSinglesPoints
    {
        [Test]
        public void Test()
        {
            List<Player> p = new List<Player>() { new Player(new Member()), new Player(new Member()) };
            p[0].BadmintonPlayerId = 03082601; p[0].Rankings = new PlayerRanking();
            Parser parser = new Parser();

            parser.UpdatePlayers(p);
            int actual = p[0].Rankings.SinglesPoints;
            System.Console.WriteLine("Actual: " + actual);

            Assert.AreNotEqual(0, actual);
        }
    }
    [TestFixture]
    [Parallelizable]
    public class ParserUpdatesMensDoublesPoints
    {
        [Test]
        public void Test()
        {
            List<Player> p = new List<Player>() { new Player(new Member()), new Player(new Member()) };
            p[0].BadmintonPlayerId = 96021601; p[0].Rankings = new PlayerRanking();
            Parser parser = new Parser();

            parser.UpdatePlayers(p);
            int actual = p[0].Rankings.DoublesPoints;
            System.Console.WriteLine("Actual: " + actual);

            Assert.AreNotEqual(0, actual);
        }
    }
    [TestFixture]
    [Parallelizable]
    public class ParserUpdatesWomensDoublesPoints
    {
        [Test]
        public void Test()
        {
            List<Player> p = new List<Player>() { new Player(new Member()), new Player(new Member()) };
            p[0].BadmintonPlayerId = 96021601; p[0].Rankings = new PlayerRanking();
            Parser parser = new Parser();

            parser.UpdatePlayers(p);
            int actual = p[0].Rankings.DoublesPoints;
            System.Console.WriteLine("Actual: " + actual);

            Assert.AreNotEqual(0, actual);
        }
    }
    [TestFixture]
    [Parallelizable]
    public class ParserUpdatesMensMixedDoublesPoints
    {
        [Test]
        public void Test()
        {
            List<Player> p = new List<Player>() { new Player(new Member()), new Player(new Member()) };
            p[0].BadmintonPlayerId = 96021601; p[0].Rankings = new PlayerRanking();
            Parser parser = new Parser();

            parser.UpdatePlayers(p);
            int actual = p[0].Rankings.MixPoints;
            System.Console.WriteLine("Actual: " + actual);

            Assert.AreNotEqual(0, actual);
        }
    }
    [TestFixture]
    [Parallelizable]
    public class ParserUpdatesWomensMixedDoulesPoints
    {
        [Test]
        public void Test()
        {
            List<Player> p = new List<Player>() { new Player(new Member()), new Player(new Member()) };
            p[0].BadmintonPlayerId = 79122601; p[0].Rankings = new PlayerRanking();

            Parser parser = new Parser();

            parser.UpdatePlayers(p);
            int actual = p[0].Rankings.MixPoints;
            System.Console.WriteLine("Actual: " + actual);

            Assert.AreNotEqual(0, actual);
        }
    }
    [TestFixture]
    [Parallelizable]
    public class ParserDoesNotUpdateIfThereIsNoPointsInSingles
    {
        [Test]
        public void Test()
        {
            List<Player> p = new List<Player>() { new Player(new Member()), new Player(new Member()) };
            p[0].BadmintonPlayerId = 96021601; p[0].Rankings = new PlayerRanking();

            Parser parser = new Parser();

            parser.UpdatePlayers(p);
            int actual = p[0].Rankings.SinglesPoints;
            System.Console.WriteLine("Actual: " + actual);

            Assert.AreEqual(0, actual);
        }
    }
    [TestFixture]
    [Parallelizable]
    public class ParserDoesNotUpdateIfThereIsNoPointsInDoubles
    {
        [Test]
        public void Test()
        {
            List<Player> p = new List<Player>() { new Player(new Member()), new Player(new Member()) };
            p[0].BadmintonPlayerId = 93062527; p[0].Rankings = new PlayerRanking();

            Parser parser = new Parser();

            parser.UpdatePlayers(p);
            int actual = p[0].Rankings.DoublesPoints;
            System.Console.WriteLine("Actual: " + actual);

            Assert.AreEqual(0, actual);
        }
    }
    [TestFixture]
    [Parallelizable]
    public class ParserUsesCorrectVersion
    {
        [Test]
        public void Test()
        {
            List<Player> p = new List<Player>() { new Player(new Member()), new Player(new Member()) };
            p[0].BadmintonPlayerId = 93062527; p[0].Rankings = new PlayerRanking();

            Parser parser = new Parser();

            parser.UpdatePlayers(p);
            int actual = p[0].Rankings.DoublesPoints;
            System.Console.WriteLine("Actual: " + actual);

            Assert.AreEqual(0, actual);
        }
    }
}