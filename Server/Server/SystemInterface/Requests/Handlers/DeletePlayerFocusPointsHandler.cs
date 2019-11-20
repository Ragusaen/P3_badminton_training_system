using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using NLog;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class DeletePlayerFocusPointsHandler : MiddleRequestHandler<DeletePlayerFocusPointRequest, DeletePlayerFocusPointResponse>
    {
        public static Logger _log = LogManager.GetCurrentClassLogger();
        protected override DeletePlayerFocusPointResponse InnerHandle(DeletePlayerFocusPointRequest request, member requester)
        {
            var db = new DatabaseEntities();
            db.SaveChanges();

            var dbPlayer = db.members.Find(request.MemberId);
            var dbFocusPoint = db.focuspoints.Find(request.FocusPointId);

            var output = new DeletePlayerFocusPointResponse
            {
                WasSuccessful = dbPlayer.focuspoints.Remove(dbFocusPoint)
            };

            _log.Debug($"Remove focus point {dbFocusPoint.Name} from player: {dbPlayer.Name} result: {output.WasSuccessful}");

            db.SaveChanges();

            return output;
        }
    }
}
