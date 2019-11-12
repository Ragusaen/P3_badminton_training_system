using System;
using System.Collections.Generic;
using System.Text;

namespace application.Model
{
    class FocusPoint
    {
        public string Description { get; set; }
        public FocusPoint(string description)
        {
            Description = description;
        }
    }
}
