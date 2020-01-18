using application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.SystemInterface;
using Common.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccountPage
    {
        private CreateAccountViewModel _vm;
        public CreateAccountPage(RequestCreator requestCreator) : base(requestCreator)
        {
            InitializeComponent();

            //Automatically go to next loginstep
            Username.Completed += (s, a) => Password.Focus();
            Password.Completed += (s, a) => ConfirmPassword.Focus();
            ConfirmPassword.Completed += (s, a) => NameSearch.Focus();

            //Show error message if Password and ConfirmPassword is not the same
            ConfirmPassword.TextChanged += (s, a) => ConfirmPasswordErrorText.IsVisible = (Password.Text != ConfirmPassword.Text);

            //Calls NotOnListCheckChanged if Checkbox is clicked
            NotOnList.CheckedChanged += (s, a) => NotOnListCheckChanged(NotOnList.IsChecked);

            //If Username is taken Username gets focus
            CreateAccountButton.Clicked += (s,a) =>
            {
                UsernameError.Focus();
                Username.Focus();
            };

            // Allow user to click on label to tick checkbox
            var notOnListLabelTap = new TapGestureRecognizer();
            notOnListLabelTap.Tapped += (s, a) => NotOnList.IsChecked = !NotOnList.IsChecked;
            NotOnListLabel.GestureRecognizers.Add(notOnListLabelTap);

            //If search text is changed the selected player is unselected
            NameSearch.TextChanged += (s, a) => _vm.PlayerUnselect();

            //Sets BindingContext ViewModel
            _vm = new CreateAccountViewModel(requestCreator, Navigation);
            BindingContext = _vm;
            _vm.Navigation = Navigation;
        }

        //Player selected
        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            NameSearch.Text = ((Player) e.SelectedItem).Member.Name;
            _vm.PlayerSelected((Player) e.SelectedItem);
        }

        //PLayerList visibility changed 
        private void NotOnListCheckChanged(bool newState)
        {
            PlayerList.IsVisible = !newState;
            PlayerListLabel.IsVisible = !newState;
        }

    }
}