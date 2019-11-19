using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.DAL;
using NLog;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetAllFocusPointDescriptorsHandler : MiddleRequestHandler<GetAllFocusPointsRequest, GetAllFocusPointsResponse>
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        protected override GetAllFocusPointsResponse InnerHandle(GetAllFocusPointsRequest request, member requester)
        {
            var db = new DatabaseEntities();

            var output = db.focuspoints.ToList().Select(p => (Common.Model.FocusPointDescriptor) p).ToList();

            _log.Debug($"Found {output.Count} focus point descriptors");

            return new GetAllFocusPointsResponse
            {
                FocusPointDescriptors = output
            };
        }
    }
}
