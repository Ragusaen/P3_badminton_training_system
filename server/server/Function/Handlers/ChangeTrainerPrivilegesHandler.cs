using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class ChangeTrainerPrivilegesHandler : MiddleRequestHandler<ChangeTrainerPrivilegesRequest, ChangeTrainerPrivilegesResponse>
    {
        protected override ChangeTrainerPrivilegesResponse InnerHandle(ChangeTrainerPrivilegesRequest request, member requester)
        {
            if (!((MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
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
                // Remove the member as trainer from all practice sessions, team matches and practice teams
                foreach (var team in dbMember.practiceteamstrainer)
                {
                    team.trainer = null;
                }
                foreach (var practicesession in dbMember.practicesessions)
                {
                    practicesession.trainer = null;
                }
                foreach (var match in dbMember.teammatches)
                {
                    match.captain = null;
                }

                _log.Debug($"Member: {request.Member.Name} has been released from Trainer Type");
            }

            db.SaveChanges();
            return new ChangeTrainerPrivilegesResponse();
        }
    }
}
