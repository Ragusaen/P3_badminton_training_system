using Server.Controller;
using Server.Model;
using Server.RankingsParser;
using System;
using System.Collections.Generic;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>() { new Player(new Member()), new Player(new Member()) };
            players[0].PlayerId = 96021601; players[0].Rankings = new PlayerRanking(); players[0].Rankings.LevelPoints = 50;
            players[1].PlayerId = 97022603; players[1].Rankings = new PlayerRanking(); players[1].Rankings.LevelPoints = 50;
            Parser parser = new Parser();

            parser.UpdatePlayers(players);

            Console.WriteLine("Done");
        }
    }
}
