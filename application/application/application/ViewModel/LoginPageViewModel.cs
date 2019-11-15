using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using application.UI;

namespace application.ViewModel
{
    class LoginPageViewModel : BaseViewModel
    {
        private string _username;

        public string Username
        {
            get { return _username; }
            set { if (SetProperty(ref _username, value))
                    LoginClickCommand.RaiseCanExecuteChanged(); }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set { if (SetProperty(ref _password, value))
                    LoginClickCommand.RaiseCanExecuteChanged(); }
        }

        private RelayCommand _loginClickCommand;

        public RelayCommand LoginClickCommand
        {
            get
            {
                return _loginClickCommand ?? (_loginClickCommand = new RelayCommand(param => ExecuteLoginClick(param), param => CanExecuteLoginClick(param)));
            }
        }

        private bool CanExecuteLoginClick(object param)
        {
            if((Password == null || Password == "") || (Username == null || Username == ""))
                return false;
            else
                return true;
        }

        //Check if user is in database. Navigate to main page.
        private void ExecuteLoginClick(object param)
        {
            Application.Current.MainPage = new NavigationPage(new MenuPage());
        }

        private RelayCommand _forgotPassWordClickCommand;

        public RelayCommand ForgotPassWordClickCommand
        {
            get
            {
                return _forgotPassWordClickCommand ?? (_forgotPassWordClickCommand = new RelayCommand(param => ExecuteForgotPassWordClick(param)));
            }
        }

        //Check if user is in database. Navigate to main page.
        private void ExecuteForgotPassWordClick(object param)
        {
            Navigation.PushAsync(new ForgotPasswordPage());
        }

        private RelayCommand _createAccountClickCommand;

        public RelayCommand CreateAccountClickCommand
        {
            get
            {
                return _createAccountClickCommand ?? (_createAccountClickCommand = new RelayCommand(param => ExecuteCreateAccountClick(param)));
            }
        }

        //Check if user is in database. Navigate to main page.
        private void ExecuteCreateAccountClick(object param)
        {
            CreateAccountViewModel vm = new CreateAccountViewModel();
            Navigation.PushAsync(new CreateAccountPage() { BindingContext = vm });
            vm.Navigation = Navigation;
        }
    }
}
