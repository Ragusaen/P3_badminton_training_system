using System.Linq;
using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class ChangeTrainerPrivilegesHandler : MiddleRequestHandler<ChangeTrainerPrivilegesRequest, ChangeTrainerPrivilegesResponse>
    {
        protected override ChangeTrainerPrivilegesResponse InnerHandle(ChangeTrainerPrivilegesRequest request, member requester)
        {
            if (!((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
            {
                return new ChangeTrainerPrivilegesResponse { AccessDenied = true };
            }

            var db = new DatabaseEntities();
            var dbMember = db.members.Find(request.Member.Id);

            if (dbMember == null)
                return new ChangeTrainerPrivilegesResponse()
                {
                    Error = "Trainer did not exist"
                };

            // Update the membertype in the database
            dbMember.MemberType = (int)request.Member.MemberType;

            // Check if it was made trainer or was unmade trainer
            if (request.Member.MemberType.HasFlag(MemberType.Trainer))
            {
                _log.Debug($"Member: {request.Member.Name} has been made Trainer Type");
            }
            else
            {
                // Remove the member as trainer from all practices
                foreach (var practiceteam in db.practiceteams.Where(p => p.trainer.ID == request.Member.Id))
                {
                    practiceteam.trainer = null;
                }

                _log.Debug($"Member: {request.Member.Name} has been released from Trainer Type");
            }

            db.SaveChanges();
            return new ChangeTrainerPrivilegesResponse();
        }
    }
}
