using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    interface Position
    {
        bool Legal { get; set; }
        List<Player> Player { get; set; }
    }
}
