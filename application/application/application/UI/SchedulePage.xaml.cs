using application.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            Navigation.PushAsync(new PlaySessionPage(((ScheduleViewModel.PlaySessionEvent) e.SelectedItem).PlaySession));

            ((ListView) sender).SelectedItem = null;
        }
    }
}