using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetFocusPointDescriptorHandler : MiddleRequestHandler<GetFocusPointDescriptorRequest, GetFocusPointDescriptorResponse>
    {
        protected override GetFocusPointDescriptorResponse InnerHandle(GetFocusPointDescriptorRequest request, member requester)
        {
            var db = new DatabaseEntities();

            return new GetFocusPointDescriptorResponse
            {
                FocusPointDescriptor = (Common.Model.FocusPointDescriptor) db.focuspoints.Find(request.Id)
            };
        }
    }
}
