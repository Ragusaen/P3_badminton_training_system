using application.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.ViewModel
{
    class CreateAccountViewModel : BaseViewModel
    {
        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                if (SetProperty(ref _username, value))
                    CreateAccountClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                if (SetProperty(ref _password, value))
                    CreateAccountClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                if (SetProperty(ref _confirmPassword, value))
                    CreateAccountClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _confirmationOfPasswordText;

        public string ConfirmationOfPasswordText
        {
            get { return _confirmationOfPasswordText; }
            set
            {
                if (SetProperty(ref _confirmationOfPasswordText, value))
                {
                    if (Password == ConfirmPassword)
                        _confirmationOfPasswordText = " ";
                    else if (Password != ConfirmPassword)
                        _confirmationOfPasswordText = "The two password you have enter are not identical";
                }
                    
            }
        }

        private RelayCommand _createAccountClickCommand;

        public RelayCommand CreateAccountClickCommand
        {
            get
            {
                return _createAccountClickCommand ?? (_createAccountClickCommand = new RelayCommand(param => ExecuteCreateAccountClick(param), param => CanExecuteCreateAccountClick(param)));
            }
        }

        private bool CanExecuteCreateAccountClick(object param)
        {
            if ((Password == null || Password == "") || (Username == null || Username == "") || (ConfirmPassword != Password))
            {
                /*if (Password == null || Password == "")
                    PasswordErrorText = "Please enter a password";
                else if (Username == null || Username == "")
                    UsernameErrorText = "Please enter a valid username";
                else if (Username == null || Username == "")
                    ConfirmationOfPasswordText = "The two password you have enter are not identical";*/
                return false;
            }
            else 
            {
                
                return true;
            }
        }

        private void ExecuteCreateAccountClick(object param)
        {
            Navigation.PushAsync(new ProfilePage());
        }

    }
}
