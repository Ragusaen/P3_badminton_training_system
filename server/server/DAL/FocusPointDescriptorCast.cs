using Common.Model;

namespace server.DAL
{
    partial class focuspoint
    {
        public static explicit operator Common.Model.FocusPointDescriptor(focuspoint fp)
        {
            return new FocusPointDescriptor
            {
                Id = fp.ID,
                Name = fp.Name,
                IsPrivate = fp.IsPrivate,
                Description = fp.Description,
                VideoURL = fp.VideoURL
            };
        }

        public static explicit operator focuspoint(Common.Model.FocusPointDescriptor fp)
        {
            return new focuspoint
            {
                Name = fp.Name,
                IsPrivate = fp.IsPrivate,
                Description = fp.Description,
                VideoURL = fp.VideoURL
            };
        }
    }
}
