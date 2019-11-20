using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using NLog;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class CreateFocusPointDescriptorHandler : MiddleRequestHandler<CreateFocusPointDescriptorRequest, CreateFocusPointDescriptorResponse>
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
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
            _log.Debug($"Received new {request.FocusPointDescriptor.GetType()} request: {request.FocusPointDescriptor.Name}");
            _log.Debug($"new FocusPointDescriptor {dbFp.Name}: {dbFp.Description}");

            db.SaveChanges();

            return new CreateFocusPointDescriptorResponse
            {
                FocusPointDescriptor = (Common.Model.FocusPointDescriptor) dbFp,
            };
        }
    }
}
