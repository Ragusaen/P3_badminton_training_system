﻿using System;
using System.Reflection;

namespace Common.Model
{
    public abstract class MemberRole
    {
        public Member Member { get; set; }
    }

    [Flags]
    public enum MemberType { None, Player, Trainer, Both = Player | Trainer }
}
