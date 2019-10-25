using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using application.UI;

namespace application.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { if (SetProperty(ref _userName, value))
                    LoginClickCommand.RaiseCanExecuteChanged(); }
        }
        private string _passWord;

        public string PassWord
        {
            get { return _passWord; }
            set { if (SetProperty(ref _passWord, value))
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
            if((PassWord == null || PassWord == "") || (UserName == null || UserName == ""))
                return false;
            else
                return true;
        }

        //Check if user is in database. Navigate to main page.
        private void ExecuteLoginClick(object param)
        {
            ScheduleViewModel vm = new ScheduleViewModel();
            Navigation.PushAsync( new SchedulePage() {BindingContext = vm});
            vm.Navigation = Navigation;
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
            ForgotPasswordViewModel vm = new ForgotPasswordViewModel();
            Navigation.PushAsync(new ForgotPasswordPage() { BindingContext = vm });
            vm.Navigation = Navigation;
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
