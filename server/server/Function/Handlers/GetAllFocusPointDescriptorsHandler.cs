using System.Linq;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class GetAllFocusPointDescriptorsHandler : MiddleRequestHandler<GetAllFocusPointsRequest, GetAllFocusPointsResponse>
    {
        protected override GetAllFocusPointsResponse InnerHandle(GetAllFocusPointsRequest request, member requester)
        {
            var db = new DatabaseEntities();

            var output = db.focuspoints.ToList().Where(p => !p.IsPrivate).Select(p => (Common.Model.FocusPointDescriptor) p).ToList();

            _log.Debug($"Found {output.Count} non-private focus point descriptors");

            return new GetAllFocusPointsResponse
            {
                FocusPointDescriptors = output
            };
        }
    }
}
