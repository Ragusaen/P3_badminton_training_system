using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class AddPlayerFocusPointHandler : MiddleRequestHandler<AddPlayerFocusPointRequest, AddPlayerFocusPointResponse>
    {
        protected override AddPlayerFocusPointResponse InnerHandle(AddPlayerFocusPointRequest request,
            member requester)
        {
            // Only allow request if it is a trainer or the player itself
            if (!(((Common.Model.MemberType) requester.MemberType).HasFlag(MemberType.Trainer) ||
                  requester.ID == request.Player.Member.Id))
            {
                RequestMember = request.Player.Member;
                return new AddPlayerFocusPointResponse {AccessDenied = true};
            }

            var db = new DatabaseEntities();
            var fp = request.FocusPointDescriptor;
            var dbFp = db.focuspoints.Find(request.FocusPointDescriptor.Id);
            var dbPlayer = db.members.Find(request.Player.Member.Id);
            db.SaveChanges();

            if (dbFp == null) // if focus point is completely new create a new one
            {
                dbFp = new focuspoint
                {
                    Name = fp.Name,
                    Description = fp.Description,
                    IsPrivate = fp.IsPrivate,
                    VideoURL = fp.VideoURL,
                };

                dbPlayer.focuspoints.Add(dbFp);
                _log.Debug($"New FocusPointDescriptor: {dbFp.Name}: {dbFp.Description}");
                db.SaveChanges();
            }
            else // otherwise add the existing one
            {
                dbPlayer.focuspoints.Add(dbFp);
            }
            _log.Debug($"{dbPlayer.Name} assigned FocusPointItem: {dbFp.Name}");

            db.SaveChanges();
            return new AddPlayerFocusPointResponse {WasSuccessful = true};
        }
    }
}
