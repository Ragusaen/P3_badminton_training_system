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
    class GetAllFocusPointDescriptorsHandler : MiddleRequestHandler<GetAllFocusPointsRequest, GetAllFocusPointsResponse>
    {
        protected override GetAllFocusPointsResponse InnerHandle(GetAllFocusPointsRequest request, member requester)
        {
            var db = new DatabaseEntities();
            return new GetAllFocusPointsResponse
            {
                FocusPointDescriptors = db.focuspoints.ToList().Select(p => (Common.Model.FocusPointDescriptor) p).ToList()
            };
        }
    }
}
