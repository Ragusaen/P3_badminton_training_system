using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class SetNonPrivateFocusPointHandler : MiddleRequestHandler<SetNonPrivateFocusPointRequest, SetNonPrivateFocusPointResponse>
    {
        protected override SetNonPrivateFocusPointResponse InnerHandle(SetNonPrivateFocusPointRequest request, member requester)
        {
            if (!((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
            {
                return new SetNonPrivateFocusPointResponse { AccessDenied = true };
            }

            var newFp = request.FocusPointDescriptor;
            var db = new DatabaseEntities();
            var dbFp = new focuspoint
            {
                Name = newFp.Name,
                IsPrivate = false,
                Description = newFp.Description,
                VideoURL = newFp.VideoURL,
            };
            db.focuspoints.Add(dbFp);

            return new SetNonPrivateFocusPointResponse();
        }
    }
}
