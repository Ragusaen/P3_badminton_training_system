using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    partial class focuspoint
    {
        public static explicit operator Common.Model.FocusPointDescriptor(Server.DAL.focuspoint fp)
        {
            return new FocusPointDescriptor
            {
                ID = fp.ID,
                Name = fp.Name,
                IsPrivate = fp.IsPrivate,
                Description = fp.Description,
                VideoURL = fp.VideoURL
            };
        }
    }
}
