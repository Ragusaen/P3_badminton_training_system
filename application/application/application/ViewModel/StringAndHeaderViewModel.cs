using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Common.Model;

namespace application.ViewModel
{
    class StringAndHeaderViewModel : BaseViewModel
    {
        public string Header
        {
            get => _header; 
            set => SetProperty(ref _header, value);
        }

        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }
        private string _header;
        private string _content;

        public StringAndHeaderViewModel(FocusPointDescriptor focusPoint)
        {
            Header = focusPoint.Name;
            if (focusPoint.Description != null)
                Content += focusPoint.Description;
            if (focusPoint.VideoURL != null)
            {
                Content += "\n \n Link:\n";
                Content += focusPoint.VideoURL;
            }

            if (Content == null)
            {
                Content += "Description is empty...";
            }
        }
    }
}
