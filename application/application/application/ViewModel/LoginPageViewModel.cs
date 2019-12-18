using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using application.SystemInterface;
using Xamarin.Forms;
using application.UI;
using Common.Model;
using Common.Serialization;

namespace application.ViewModel
{
    class LoginPageViewModel : BaseViewModel
    {
        public LoginPageViewModel(RequestCreator requestCreator, INavigation navigation) : base(requestCreator,
            navigation)
        {

        }

        #region InvalidLogin
        private bool _invalidLoginTextVisible;

        public bool InvalidLoginTextVisible
        {
            get { return _invalidLoginTextVisible; }
            set { SetProperty(ref _invalidLoginTextVisible, value); }
        }

        private int _invalidLoginTextHeight;
        private const int TextHeight = 20;

        public int InvalidLoginTextHeight
        {
            get { return _invalidLoginTextHeight; }
            set { SetProperty(ref _invalidLoginTextHeight, value); }
        }
        #endregion

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
            return !(string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username));
        }

        ////Check if user is in database. Navigate to main page.
        private void ExecuteLoginClick(object param)
        {
            if (RequestCreator.LoginRequest(Username, Password)) { 
                RequestCreator.LoggedInMember = RequestCreator.GetLoggedInMember();
                Application.Current.MainPage = new NavigationPage(new MenuPage(RequestCreator));
            }
            else
            {
                InvalidLoginTextHeight = TextHeight;
                InvalidLoginTextVisible = true;
            }
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
            Navigation.PushAsync(new ForgotPasswordPage(RequestCreator));
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
            Navigation.PushAsync(new CreateAccountPage(RequestCreator));
        }
    }
}
