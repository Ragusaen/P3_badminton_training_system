using application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccountPage : ContentPage
    {
        private CreateAccountViewModel _vm;
        public CreateAccountPage()
        {
            InitializeComponent();

            Username.Completed += (s, a) => Password.Focus();
            Password.Completed += (s, a) => ConfirmPassword.Focus();
            ConfirmPassword.Completed += (s, a) => NameSearch.Focus();

            ConfirmPassword.TextChanged += (s, a) => ConfirmPasswordErrorText.IsVisible = (Password.Text != ConfirmPassword.Text);

            NotOnList.CheckedChanged += (s, a) => NotOnListCheckChanged(NotOnList.IsChecked);

            CreateAccountButton.Clicked += (s,a) =>
            {
                UsernameError.Focus();
                Username.Focus();
            };

            // Allow user to click on label to tick checkbox
            var notOnListLabelTap = new TapGestureRecognizer();
            notOnListLabelTap.Tapped += (s, a) => NotOnList.IsChecked = !NotOnList.IsChecked;
            NotOnListLabel.GestureRecognizers.Add(notOnListLabelTap);

            NameSearch.TextChanged += (s, a) => _vm.PlayerUnselect();

            _vm = new CreateAccountViewModel();
            BindingContext = _vm;
            _vm.Navigation = Navigation;
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            NameSearch.Text = ((Player) e.SelectedItem).Member.Name;
            _vm.PlayerSelected((Player) e.SelectedItem);
        }

        private void NotOnListCheckChanged(bool newState)
        {
            PlayerList.IsVisible = !newState;
            PlayerListLabel.IsVisible = !newState;
        }

    }
}