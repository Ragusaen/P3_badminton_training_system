using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
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
