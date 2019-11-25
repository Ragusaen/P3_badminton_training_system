using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class SetCommentHandler : MiddleRequestHandler<SetCommentRequest, SetCommentResponse>
    {
        protected override SetCommentResponse InnerHandle(SetCommentRequest request, member requester)
        {
            if (!(((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer) ||
                  requester.ID == request.Member.Id))
            {
                RequestMember = request.Member;
                return new SetCommentResponse { AccessDenied = true };
            }

            var db = new DatabaseEntities();
            var m = db.members.Find(request.Member.Id);
            _log.Debug($"Member: {m.Name} received new comment {request.NewComment}");
            m.Comment = request.NewComment;
            db.SaveChanges();

            return new SetCommentResponse();
        }
    }
}
