using System.Collections.Generic;

namespace Common.Model
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MemberType MemberType { get; set; }
        public string Comment { get; set; }
    }
}
