using application.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.ViewModel
{
    class CreateAccountViewModel : BaseViewModel
    {
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (SetProperty(ref _userName, value))
                    CreateAccountContinueClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _passWord;

        public string PassWord
        {
            get { return _passWord; }
            set
            {
                if (SetProperty(ref _passWord, value))
                    CreateAccountContinueClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                if (SetProperty(ref _email, value))
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
            if ((PassWord == null || PassWord == "") || (UserName == null || UserName == "") || (Email == null || Email == ""))
                return false;
            else
                return true;
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
