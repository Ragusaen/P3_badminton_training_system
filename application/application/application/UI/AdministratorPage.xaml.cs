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
        //Sets BindingContext ViewModel
        private AdministratorViewModel _vm;
        public AdministratorPage()
        {
            InitializeComponent();
            _vm = new AdministratorViewModel();
            _vm.Navigation = Navigation;
            BindingContext = _vm;

            FocusPointList.ItemAppearing += (s, r) => LoadEditIcons();
            FocusPointList.ItemDisappearing += (s, r) => LoadEditIcons();
        }
        //Loads Icon
        private void LoadEditIcons()
        {
            foreach (var templatedItem in FocusPointList.TemplatedItems)
            {
                ((ImageButton)templatedItem.FindByName("EditButton")).Source = ImageSource.FromResource("application.Images.editicon.png");
            }
        }

        //Click on Member navigates to Members ProfilePage
        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new ProfilePage(((Member)e.SelectedItem).Id));
        }
        //Click on Team navigates to Teams PracticeTeamPage
        private void PracticeTeamListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is PracticeTeam team)
                Navigation.PushAsync(new PracticeTeamPage(team.Id));
            PracticeTeamList.SelectedItem = null;
        }
        //Click on FocusPoint navigates to FocusPoint ViewFocusPointsDetailPupupPage
        private void FocusPointListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var focusPoint = (FocusPointDescriptor)e.SelectedItem;
            if (focusPoint != null)
                _vm.PopupFocusPoint(focusPoint);
            FocusPointList.SelectedItem = null;
        }
    }
}