using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class YearPlanSection
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<FocusPointDescriptor> FocusPoints { get; set; }
    }
}
