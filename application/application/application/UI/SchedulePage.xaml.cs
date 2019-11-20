using application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Controls;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : ContentPage
    {
        public SchedulePage()
        {
            InitializeComponent();

            ScheduleViewModel vm = new ScheduleViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;
            Plusicon.Source = ImageSource.FromResource("application.Images.plusicon.jpg");

        }
    }
}