using System;
using System.Collections.Generic;
using System.Text;
using application.SystemInterface;
using Xamarin.Forms;

namespace application.UI
{
    public class BasePage : ContentPage
    {
        public RequestCreator RequestCreator;

        public BasePage(RequestCreator requestCreator)
        {
            RequestCreator = requestCreator;
        }
    }
}
