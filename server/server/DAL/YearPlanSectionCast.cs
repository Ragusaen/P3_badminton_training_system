using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    partial class yearplansection
    {
        public static explicit operator Common.Model.YearPlanSection(Server.DAL.yearplansection yps)
        {
            return new YearPlanSection
            {
                Id = yps.ID,
                StartDate = yps.StartDate,
                EndDate = yps.EndDate,
                FocusPoint = new FocusPointItem()
                {
                    Descriptor = (FocusPointDescriptor)yps.focuspoint,
                }
            };
        }
    }
}
