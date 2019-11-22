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
    class SetPlayerFocusPointsHandler : MiddleRequestHandler<SetPlayerFocusPointsRequest, SetPlayerFocusPointsResponse>
    {
        protected override SetPlayerFocusPointsResponse InnerHandle(SetPlayerFocusPointsRequest request,
            member requester)
        {

            if (!(((Common.Model.MemberType) requester.MemberType).HasFlag(MemberType.Trainer) ||
                  requester.ID == request.Player.Member.Id))
            {
                RequestMember = request.Player.Member;
                return new SetPlayerFocusPointsResponse {AccessDenied = true};
            }

            var db = new DatabaseEntities();
            var dbPlayer = db.members.Find(request.Player.Member.Id);

            if (dbPlayer == null)
                return new SetPlayerFocusPointsResponse {WasSuccessful = false};

            foreach (var fp in request.FocusPoints)
            {
                var dbFp = db.focuspoints.Find(fp.Descriptor.Id);

                if (dbFp == null) // if focus point is completely new
                {
                    dbPlayer.focuspoints.Add((Server.DAL.focuspoint) fp.Descriptor);
                    _log.Debug($"New FocusPointDescriptor: {fp.Descriptor.Name}: {fp.Descriptor.Description}");
                }
                else if (dbPlayer.focuspoints.SingleOrDefault(p => p.ID == dbFp.ID) == null
                ) // if focus is not already assigned to player
                {
                    dbPlayer.focuspoints.Add(dbFp);
                    _log.Debug($"{dbPlayer.Name} assigned FocusPointItem: {fp.Descriptor.Name}");
                }
            }

            db.SaveChanges();
            return new SetPlayerFocusPointsResponse {WasSuccessful = true};
        }
    }
}
