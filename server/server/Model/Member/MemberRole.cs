using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public abstract class MemberRole
    {
        public Member Member { get; private set; }

        public MemberRole(Member member)
        {
            this.Member = member;
        }
    }
}
