using NUnit.Framework;
using server.RankingsParser;
using server.Model;

namespace Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Player player = new Player(new Member());
            Assert.Pass();
        }
    }
}