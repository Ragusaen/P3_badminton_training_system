using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Positions
{
    class MixDouble : IPosition
    {
        public bool Legal { get; set; }
        public List<Player> Player { get; set; } = new List<Player>();
    }
}
