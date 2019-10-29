using System;
using System.Collections.Generic;
using System.Text;

namespace application.Model
{
    class PracticeTeam
    {
        public string Name { get; set; }

        public bool OnTeam { get; set; }

        public PracticeTeam(string name, bool onTeam)
        {
            Name = name;
            OnTeam = onTeam;
        }
    }
}
