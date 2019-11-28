using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using application.UI;
using application.SystemInterface.Network;
using application.SystemInterface;
using System.Diagnostics;
using application.ViewModel;
using SkiaSharp;

namespace application
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            try
            {
                RequestCreator.Connect();
            }
            catch (FailedToConnectToServerException)
            {
                MainPage = new ConnectionFailedPage();
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
