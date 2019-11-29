using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
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
                var players = fp.members;
                var pracsMain = fp.practicesessionsmain;
                var pracsSub = fp.practicesessionssub;

                foreach (var p in players)
                    p.focuspoints.Remove(fp);

                foreach (var prac in pracsMain)
                    prac.mainfocuspoint = null;

                foreach (var prac in pracsSub)
                    prac.subfocuspoints.Remove(fp);

                db.focuspoints.Remove(fp);
                _log.Debug($"Completely removed focus point descriptor {fp.Name}");
            }

            db.SaveChanges();
            return new DeleteFocusPointDescriptorResponse();
        }
    }
}
