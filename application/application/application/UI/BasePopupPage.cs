using System;
using System.Collections.Generic;
using System.Text;
using application.SystemInterface;
using Rg.Plugins.Popup.Pages;

namespace application.UI
{
    public class BasePopupPage : PopupPage
    {
        public RequestCreator RequestCreator;

        public BasePopupPage(RequestCreator requestCreator)
        {
            RequestCreator = requestCreator;
        }
    }
}
