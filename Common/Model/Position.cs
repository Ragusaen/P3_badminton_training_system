﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class Position
    {
        public Player Player { get; set; }
        public bool IsExtra { get; set; }
        public Player OtherPlayer { get; set; }
        public bool OtherIsExtra { get; set; }
    }
}
