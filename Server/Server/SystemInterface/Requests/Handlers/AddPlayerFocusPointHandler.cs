using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using NLog;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class AddPlayerFocusPointHandler : MiddleRequestHandler<AddPlayerFocusPointRequest, AddPlayerFocusPointResponse>
    {
        protected override AddPlayerFocusPointResponse InnerHandle(AddPlayerFocusPointRequest request,
            member requester)
        {
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

            if (dbFp == null) // if focus point is completely new
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
            else
            {
                dbPlayer.focuspoints.Add(dbFp);
            }
            _log.Debug($"{dbPlayer.Name} assigned FocusPointItem: {dbFp.Name}");

            db.SaveChanges();
            return new AddPlayerFocusPointResponse {WasSuccessful = true};
        }
    }
}
