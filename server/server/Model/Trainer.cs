using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    class Trainer : MemberRole
    {
        private List<PlaySession> _playSessions = new List<PlaySession>();

        public Trainer(Member member) : base(member)
        {
            
        }

        public void AssignToPlaySession(PlaySession playSession)
        {
            _playSessions.Add(playSession);
        }


    }
}
