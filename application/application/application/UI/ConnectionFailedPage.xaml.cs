using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using application.SystemInterface;
using application.SystemInterface.Network;
using Common.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnectionFailedPage
    {
        public ConnectionFailedPage(RequestCreator requestCreator) : base(requestCreator)
        {
            InitializeComponent();
            //Click to reconnect navigates to LoginPage
            ReconnectButton.Clicked += (s, a) =>
            {
                try
                {
                    if (RequestCreator.Connect())
                    {
                        Application.Current.MainPage = new NavigationPage(new LoginPage(RequestCreator));
                    }
                }
                catch (FailedToConnectToServerException) { }

            };
        }
    }
}