using System;
using System.Collections.Generic;
using System.Text;
using application.SystemInterface;
using Xamarin.Forms;

namespace application.UI
{
    public class BaseMasterDetailPage : MasterDetailPage
    {
        public RequestCreator RequestCreator;

        public BaseMasterDetailPage(RequestCreator requestCreator)
        {
            RequestCreator = requestCreator;
        }
    }
}
