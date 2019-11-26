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
        protected override ChangeTrainerPrivilegesResponse InnerHandle(ChangeTrainerPrivilegesRequest request, member requester)
        {
            if (!((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
            {
                RequestMember = request.Member;
                return new ChangeTrainerPrivilegesResponse { AccessDenied = true };
            }

            var db = new DatabaseEntities();
            db.members.Find(request.Member.Id).MemberType = (int)request.Member.MemberType;

            if (request.Member.MemberType.HasFlag(MemberType.Trainer))
            {
                _log.Debug($"Member: {request.Member.Name} has been made Trainer Type");
            }
            else
            {
                _log.Debug($"Member: {request.Member.Name} has been released from Trainer Type");
            }

            db.SaveChanges();
            return new ChangeTrainerPrivilegesResponse();
        }
    }
}
