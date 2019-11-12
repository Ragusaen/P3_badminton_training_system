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
            LoginPageViewModel vm = new LoginPageViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;
            
            RedLogo.Source = ImageSource.FromResource("application.Images.tritonlogo.jpg");
            NaviLogo.Source = ImageSource.FromResource("application.Images.logo.gif");
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {

        }
    }
}
