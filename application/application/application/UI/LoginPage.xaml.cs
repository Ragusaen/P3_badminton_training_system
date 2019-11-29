using application.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace application.UI
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            //automatically goes to next Login phase
            Username.Completed += (s,a) => Password.Focus();
            Password.Completed += (s, a) => LoginButton.SendClicked();

            //Sets BindingContext ViewModel
            LoginPageViewModel vm = new LoginPageViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;
            
            //initiates images
            RedLogo.Source = ImageSource.FromResource("application.Images.TritonLogo.png");
            NaviLogo.Source = ImageSource.FromResource("application.Images.logo.gif");
        }
    }
}
