﻿using System.Collections.Generic;

namespace Common.Model
{
    public class Member
    {
        public List<MemberRole> Roles = new List<MemberRole>();

        public int Id;
        public string Name;
    }
}