using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    class Lineup
    {
        public Match Match { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();

    }
}
