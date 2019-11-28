using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.DAL;
using Server.SystemInterface.Requests.Handlers;

namespace Server.Function.Handlers
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
