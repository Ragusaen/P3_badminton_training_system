using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    interface IPosition
    {
        bool Legal { get; set; }
        List<Player> Player { get; set; }
    }
}
