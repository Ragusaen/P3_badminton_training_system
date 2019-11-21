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
    class GetAdminPageHandler : MiddleRequestHandler<GetAdminPageRequest, GetAdminPageResponse>
    {
        protected override GetAdminPageResponse InnerHandle(GetAdminPageRequest request, member requester)
        {
            if (((MemberType) requester.MemberType).HasFlag(MemberType.Trainer))
                return new GetAdminPageResponse() {AccessDenied = true};

            var db = new DatabaseEntities();
            return new GetAdminPageResponse()
            {
                FocusPoints = db.focuspoints.ToList().Select(fp => (FocusPointDescriptor)fp).ToList(),
                Members = db.members.ToList().Select(m => (Member)m).ToList(),
                PracticeTeams = db.practiceteams.ToList().Select(pt => (PracticeTeam)pt).ToList()
            };
        }
    }
}
