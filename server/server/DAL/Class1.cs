using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server
{
    partial class member
    {
        public static implicit operator Common.Model.Member(Server.member m)
        {
            return new Member() {
                    Id = m.ID,
                    Name = m.Name
            };
        }
    }
}
