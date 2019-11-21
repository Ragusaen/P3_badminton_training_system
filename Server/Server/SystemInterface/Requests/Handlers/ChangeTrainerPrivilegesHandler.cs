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
    class ChangeTrainerPrivilegesHandler : MiddleRequestHandler<ChangeTrainerPrivilegesRequest, ChangeTrainerPrivilegesResponse>
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        protected override ChangeTrainerPrivilegesResponse InnerHandle(ChangeTrainerPrivilegesRequest request, member requester)
        {
            if (requester.MemberType != (int)MemberType.Trainer)
                return null;
            var db = new DatabaseEntities();
            db.members.Find(request.Member.Id).MemberType = (int)request.Member.MemberType;

            if ((request.Member.MemberType & MemberType.Trainer) > 0)
            {
                _log.Debug($"Member: {request.Member.Name} has been made Trainer Type");
            }
            _log.Debug($"Member: {request.Member.Name} has been released from Trainer Type");

            return new ChangeTrainerPrivilegesResponse();
        }
    }
}
