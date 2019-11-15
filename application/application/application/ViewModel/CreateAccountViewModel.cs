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
                    CreateAccountClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _passWord;

        public string PassWord
        {
            get { return _passWord; }
            set
            {
                if (SetProperty(ref _passWord, value))
                    CreateAccountClickCommand.RaiseCanExecuteChanged();
            }
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
            Navigation.PushAsync(new ProfilePage());
        }

        //Check if username is free in database.
        private void ExecuteCreateAccountContinueClick(object param)
        {
            CreateAccountChooseNameViewModel vm = new CreateAccountChooseNameViewModel();
            Navigation.PushAsync(new CreateAccountChooseNamePage() { BindingContext = vm });
            vm.Navigation = Navigation;
        }
    }
}
