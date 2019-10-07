using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    class PracticeTeam
    {
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Trainer> Trainers { get; set; } = new List<Trainer>();
        public List<Practice> Practices { get; set; } = new List<Practice>();


    }
}
