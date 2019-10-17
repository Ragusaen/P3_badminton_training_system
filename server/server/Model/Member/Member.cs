using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    class Member
    {
        private List<MemberRole> _roles = new List<MemberRole>();

        public string Name { get; set; }
        public int Sex { get; set; }
        public int Id { get; set; }

        public Member(string name, int sex)
        {
            Name = name;
            Sex = sex;
        }

        public Member(string name, int sex, int id)
        {
            Name = name;
            Sex = sex;
            Id = id;
        }

        public void AddRole<T>(T Role) where T : MemberRole
        {
            // Check if member already has role
            if (_roles.Count(f => f is T) == 0)
            {
                _roles.Add(Role);
            }
        }

        public T GetRole<T>() where T : MemberRole
        {
            var r = _roles.Where(role => role is T);
            if (r.Count() < 1)
            {
                throw new ArgumentException("Member does not have this role");
            }
            return r.First() as T;
        }

        public override string ToString()
        {
            return Id + " - " + Name;
        }
    }
}
