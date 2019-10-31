using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            
            ProfilePageViewModel vm = new ProfilePageViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;

            //Menuicon.Source = ImageSource.FromResource("application.Images.menuicon.jpg");
            Settingsicon.Source = ImageSource.FromResource("application.Images.settingsicon.jpg");

            //Sub page navigation:
            //((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new SubmitFeedbackPage());
        }
    }
}