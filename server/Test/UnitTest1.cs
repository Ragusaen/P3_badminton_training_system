using NUnit.Framework;
using Server.RankingsParser;
using Server.Model;

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