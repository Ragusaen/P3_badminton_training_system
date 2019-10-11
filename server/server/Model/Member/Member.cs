using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    class Member
    {
        private List<MemberRole> _roles = new List<MemberRole>();

        public string Name { get; set; }

        public byte Sex { get; set; }

        public int Id { get; set; }

        public void AddRole<T>(T Role) where T : MemberRole
        {
            // Check if member already has role
            if (_roles.Count(f => f is T) == 0)
            {
                _roles.Add(Role);
            }
        }
    }
}
