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
            if ((PassWord == null || PassWord == "") || (UserName == null || UserName == ""))
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
