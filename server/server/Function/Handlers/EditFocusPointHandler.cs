using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class EditFocusPointHandler : MiddleRequestHandler<EditFocusPointRequest, EditFocusPointResponse>
    {
        protected override EditFocusPointResponse InnerHandle(EditFocusPointRequest request, member requester)
        {
            var db = new DatabaseEntities();
            var dbFp = db.focuspoints.Find(request.FP.Id);
            
            dbFp.Name = request.FP.Name;
            dbFp.Description = request.FP.Description;
            dbFp.VideoURL = request.FP.VideoURL;

            db.SaveChanges();
            return new EditFocusPointResponse();
        }
    }
}
