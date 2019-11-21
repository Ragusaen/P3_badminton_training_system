using application.ViewModel;
using Common.Model;
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
    public partial class AdministratorPage : ContentPage
    {
        public AdministratorPage()
        {
            InitializeComponent();
            AdministratorViewModel vm = new AdministratorViewModel();
            vm.Navigation = Navigation;
            BindingContext = vm;
        }

        private void TeamListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is PracticeTeam Team)
                Navigation.PushAsync(new TeamPage(Team));
            TeamList.SelectedItem = null;
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new ProfilePage((Member)e.SelectedItem));
        }
    }
}