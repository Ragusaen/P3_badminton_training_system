﻿using System.Reflection;

namespace Common.Model
{
    public abstract class MemberRole
    {
        public enum Type { None, Player, Trainer }

        public Member Member;
    }
}