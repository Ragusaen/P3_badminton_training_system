using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class Team
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<Member> Members { get; set; }
    }
}
