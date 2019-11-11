using application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : ContentPage
    {
        public SchedulePage()
        {
            InitializeComponent();
            ScheduleViewModel vm = new ScheduleViewModel();
            vm.Navigation = Navigation;
            BindingContext = vm;
            Plusicon.Source = ImageSource.FromResource("application.Images.plusicon.jpg");

        }

        /*void AddPracticeSessionClicked(object sender, EventArgs e)
        {
            //Sub page navigation:
            Navigation.PushAsync(new CreatePracticePage());
            //((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new CreatePracticePage());
            //(((MasterDetailPage)Application.Current.MainPage).Detail as NavigationPage).PushAsync(new CreatePracticePage());
        }*/
    }
}