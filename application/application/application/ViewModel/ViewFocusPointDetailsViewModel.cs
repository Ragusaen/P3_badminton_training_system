using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using application.SystemInterface;
using Common.Model;
using Common.Serialization;
using Xamarin.Forms;

namespace application.ViewModel
{
    class ViewFocusPointDetailsViewModel : BaseViewModel
    {
        private string _name;
        public string Name
        {
            get => _name; 
            set => SetProperty(ref _name, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _videoUrl;

        public string VideoUrl
        {
            get => _videoUrl;
            set => SetProperty(ref _videoUrl, value);
        }

        public ViewFocusPointDetailsViewModel(FocusPointDescriptor focusPoint, RequestCreator requestCreator, INavigation navigation) : base(requestCreator, navigation)
        {
            Name = focusPoint.Name;
            Description = focusPoint.Description;
            VideoUrl = focusPoint.VideoURL;
        }
    }
}
