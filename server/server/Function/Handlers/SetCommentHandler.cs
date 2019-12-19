using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class SetCommentHandler : MiddleRequestHandler<SetCommentRequest, SetCommentResponse>
    {
        protected override SetCommentResponse InnerHandle(SetCommentRequest request, member requester)
        {
            if (!(((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer) ||
                  requester.ID == request.Member.Id))
            {
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
