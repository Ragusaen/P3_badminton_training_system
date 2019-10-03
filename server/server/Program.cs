using server.Controller;
using System;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            var bpscraper = new BPScraper();

            string r = bpscraper.ScrapeHTMLSource("https://www.badmintonplayer.dk/DBF/Ranglister/#287,2019,,0,,,,0,,,,15,,,,0,,,,,,");

            Console.WriteLine(r);
        }
    }
}
