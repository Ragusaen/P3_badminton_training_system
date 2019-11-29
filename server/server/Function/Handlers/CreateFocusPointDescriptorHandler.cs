using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class CreateFocusPointDescriptorHandler : MiddleRequestHandler<CreateFocusPointDescriptorRequest, CreateFocusPointDescriptorResponse>
    {
        protected override CreateFocusPointDescriptorResponse InnerHandle(CreateFocusPointDescriptorRequest request, member requester)
        {
            var db = new DatabaseEntities();
            var newFp = new focuspoint
            {
                Name = request.FocusPointDescriptor.Name,
                Description = request.FocusPointDescriptor.Description,
                VideoURL = request.FocusPointDescriptor.VideoURL,
                IsPrivate = request.FocusPointDescriptor.IsPrivate,
            };

            var dbFp = db.focuspoints.Add(newFp);
            _log.Debug($"new FocusPointDescriptor {dbFp.Name}: {dbFp.Description}");

            db.SaveChanges();

            return new CreateFocusPointDescriptorResponse
            {
                FocusPointDescriptor = (Common.Model.FocusPointDescriptor) dbFp,
            };
        }
    }
}
