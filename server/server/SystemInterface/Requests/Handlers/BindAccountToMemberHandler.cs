using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class BindAccountToMemberHandler : MiddleRequestHandler<BindAccountToMemberRequest, BindAccountToMemberResponse>
    {
        protected override BindAccountToMemberResponse InnerHandle(BindAccountToMemberRequest request, member requester)
        {

        }
    }
}
