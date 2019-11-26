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
        private ScheduleViewModel _vm;
        public SchedulePage()
        {
            InitializeComponent();

            _vm = new ScheduleViewModel();
            BindingContext = _vm;
            _vm.Navigation = Navigation;
            Plusicon.Source = ImageSource.FromResource("application.Images.plusicon.jpg");
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            Navigation.PushAsync(new PlaySessionPage(((ScheduleViewModel.PlaySessionEvent) e.SelectedItem).PlaySession));

            ((ListView) sender).SelectedItem = null;
        }

        private void AddPlaysession_OnClicked(object sender, EventArgs e)
        {
            DisplayAddPlaySessionActionSheet();
        }

        private async void DisplayAddPlaySessionActionSheet()
        {
            string action = await Application.Current.MainPage.DisplayActionSheet("Choose what you want to add:", "Cancel", null, "Add New Practice", "Add New Match");

            if (action == "Add New Practice")
                await Navigation.PushAsync(new CreatePracticePage(_vm.SelectedDate));
            else if (action == "Add New Match")
                await Navigation.PushAsync(new CreateMatchPage());
        }
    }
}