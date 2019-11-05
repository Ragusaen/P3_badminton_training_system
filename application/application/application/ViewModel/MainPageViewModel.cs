using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using application.UI;

using application.SystemInterface.Network;
using System.Net.Sockets;
using System.Diagnostics;
using application.SystemInterface;

namespace application.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {
        #region InvalidLogin
        private bool _invalidLoginTextVisible;

        public bool InvalidLoginTextVisible
        {
            get { return _invalidLoginTextVisible; }
            set { SetProperty(ref _invalidLoginTextVisible, value); }
        }

        private int _invalidLoginTextHeight;

        public int InvalidLoginTextHeight
        {
            get { return _invalidLoginTextHeight; }
            set { SetProperty(ref _invalidLoginTextHeight, value); }
        }
        #endregion

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

            if (RequestCreator.LoginRequest("johninator", "forty2"))
            {
                ScheduleViewModel vm = new ScheduleViewModel();
                Navigation.PushAsync(new SchedulePage() { BindingContext = vm });
                vm.Navigation = Navigation;
            } else
            {
                InvalidLoginTextHeight = 20;
                InvalidLoginTextVisible = true;
            }

        }
        private RelayCommand _forgotPassWordClickCommand;

        public RelayCommand ForgotPassWordClickCommand
        {
            get
            {
                return _forgotPassWordClickCommand ?? (_forgotPassWordClickCommand = new RelayCommand(param => ExecuteFogotPassWordClick(param)));
            }
        }

        //Check if user is in database. Navigate to main page.
        private void ExecuteFogotPassWordClick(object param)
        {
            ScheduleViewModel vm = new ScheduleViewModel();
            Navigation.PushAsync(new SchedulePage() { BindingContext = vm });
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
            ScheduleViewModel vm = new ScheduleViewModel();
            Navigation.PushAsync(new SchedulePage() { BindingContext = vm });
            vm.Navigation = Navigation;
        }
    }
}
