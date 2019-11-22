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
    class DeleteFocusPointDescriptorHandler : MiddleRequestHandler<DeleteFocusPointDescriptorRequest, DeleteFocusPointDescriptorResponse>
    {
        protected override DeleteFocusPointDescriptorResponse InnerHandle(DeleteFocusPointDescriptorRequest request, member requester)
        {
            if (!((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
            {
                return new DeleteFocusPointDescriptorResponse { AccessDenied = true };
            }
            var db = new DatabaseEntities();
            var fp = db.focuspoints.Find(request.FocusPointDescriptor.Id);
            if (fp != null)
            {
                db.focuspoints.Remove(fp);
                _log.Debug($"Completely removed focus point descriptor {fp.Name}");
            }

            db.SaveChanges();
            return new DeleteFocusPointDescriptorResponse();
        }
    }
}
