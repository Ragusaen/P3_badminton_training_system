using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Server.SystemInterface.Requests.Handlers;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class SetMemberSexHandler : MiddleRequestHandler<SetMemberSexRequest, SetMemberSexResponse>
    {
        protected override SetMemberSexResponse InnerHandle(SetMemberSexRequest request, member requester)
        {
            var db = new DatabaseEntities();

            // If requester is trainer
            if (!((MemberType) requester.MemberType).HasFlag(MemberType.Trainer))
                return new SetMemberSexResponse
                {
                    AccessDenied = true
                };

            db.members.Find(request.Player.Member.Id).Sex = (int) request.NewSex;
            db.SaveChanges();

            return new SetMemberSexResponse();
        }
    }
}
