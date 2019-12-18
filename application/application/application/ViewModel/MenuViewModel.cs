using application.UI;
using System;
using System.Collections.Generic;
using System.Text;
using application.SystemInterface;
using Common.Model;
using Xamarin.Forms;

namespace application.ViewModel
{
    class MenuViewModel : BaseViewModel
    {
        public MenuViewModel(RequestCreator requestCreator, INavigation navigation) : base(requestCreator, navigation)
        {

        }
    }
}
