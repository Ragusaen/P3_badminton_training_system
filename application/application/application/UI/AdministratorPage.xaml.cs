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
        private AdministratorViewModel _vm;
        public AdministratorPage()
        {
            InitializeComponent();
            _vm = new AdministratorViewModel();
            _vm.Navigation = Navigation;
            BindingContext = _vm;

            FocusPointList.ItemAppearing += (s, r) => LoadEditIcons();
        }

        private void LoadEditIcons()
        {
            foreach (var templatedItem in FocusPointList.TemplatedItems)
            {
                ((ImageButton)templatedItem.FindByName("EditButton")).Source = ImageSource.FromResource("application.Images.editicon.png");
            }
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new ProfilePage(((Member)e.SelectedItem).Id));
        }

        private void PracticeTeamListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is PracticeTeam team)
                Navigation.PushAsync(new PracticeTeamPage(team.Id));
            PracticeTeamList.SelectedItem = null;
        }

        private void FocusPointListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var focusPoint = (FocusPointDescriptor)e.SelectedItem;
            if (focusPoint != null)
                _vm.PopupFocusPoint(focusPoint);
            FocusPointList.SelectedItem = null;
        }
    }
}