using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using NLog;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class DeletePlayerFocusPointsHandler : MiddleRequestHandler<DeletePlayerFocusPointRequest, DeletePlayerFocusPointResponse>
    {
        protected override DeletePlayerFocusPointResponse InnerHandle(DeletePlayerFocusPointRequest request, member requester)
        {
            if (!(((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer) ||
                  requester.ID == request.Player.Member.Id))
            {
                RequestMember = request.Player.Member;
                return new DeletePlayerFocusPointResponse { AccessDenied = true };
            }
            var db = new DatabaseEntities();

            var dbPlayer = db.members.Find(request.Player.Member.Id);
            var dbFocusPoint = db.focuspoints.Find(request.FocusPointItem.Descriptor.Id);

            dbPlayer.focuspoints.Remove(dbFocusPoint);

            if (dbFocusPoint.IsPrivate)
            {
                db.focuspoints.Remove(dbFocusPoint);
                _log.Debug($"Removed private focus point {dbFocusPoint.Name} from player: {dbPlayer.Name}");
            }
            else
            {
                _log.Debug($"Removed focus point {dbFocusPoint.Name} from player: {dbPlayer.Name}");
            }

            db.SaveChanges();
            return new DeletePlayerFocusPointResponse();
        }
    }
}
