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

            Plusicon.Source = ImageSource.FromResource("application.Images.plusicon.jpg");
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var page = new PlaySessionPage(((ScheduleViewModel.PlaySessionEvent) e.SelectedItem).PlaySession);
            Navigation.PushAsync(page);

            ((ListView) sender).SelectedItem = null;
        }

        private void AddPlaysession_OnClicked(object sender, EventArgs e)
        {
            DisplayAddPlaySessionActionSheet();
        }

        private async void DisplayAddPlaySessionActionSheet()
        {
            string action = await Application.Current.MainPage.DisplayActionSheet("Choose what you want to add:", "Cancel", null, "Add New Practice", "Add New Match");

            Page page;
            if (action == "Add New Practice")
                page = new CreatePracticePage();
            else if (action == "Add New Match")
                page = new CreateMatchPage();
            else
                return;

            page.Disappearing += (s, a) =>
            {
                Navigation.InsertPageBefore(this, new SchedulePage());
                Navigation.PopAsync();
            };
            await Navigation.PushAsync(page);
                await Navigation.PushAsync(new CreateMatchPage(_vm.SelectedDate));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm = new ScheduleViewModel();
            BindingContext = _vm;
            _vm.Navigation = Navigation;
        }
    }
}