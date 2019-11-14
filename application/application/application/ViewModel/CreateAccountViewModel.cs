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
                    CreateAccountContinueClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                if (SetProperty(ref _password, value))
                    CreateAccountContinueClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _cantContinueText;

        public string CantContinueText
        {
            get { return _cantContinueText; }
            set
            {
                
                if (SetProperty( ref _cantContinueText, value))
                    CreateAccountContinueClickCommand.RaiseCanExecuteChanged();

            }
        }

        private RelayCommand _createAccountContinueClickCommand;

        public RelayCommand CreateAccountContinueClickCommand
        {
            get
            {
                return _createAccountContinueClickCommand ?? (_createAccountContinueClickCommand = new RelayCommand(param => ExecuteCreateAccountContinueClick(param), param => CanExecuteCreateAccountContinueClick(param)));
            }
        }

        private bool CanExecuteCreateAccountContinueClick(object param)
        {
            if ((Password == null || Password == "") || (Username == null || Username == ""))
            {
                CantContinueText = "You need to enter an username and a password before you can continue";
                return false;
            }
            else 
            {
                CantContinueText = " ";
                return true;
            }
        }

        //Check if username is free in database
        private void ExecuteCreateAccountContinueClick(object param)
        {
            CreateAccountChooseNameViewModel vm = new CreateAccountChooseNameViewModel();
            Navigation.PushAsync(new CreateAccountChooseNamePage() { BindingContext = vm });
            vm.Navigation = Navigation;
        }
    }
}
