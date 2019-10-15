using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace application.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private string _passWord;

        public string PassWord
        {
            get { return _passWord; }
            set { SetProperty(ref _passWord, value); }
        }

        private ICommand _loginClickCommand;

        public ICommand LoginClickCommand
        {
            get
            {
                return _loginClickCommand ?? (_loginClickCommand = new RelayCommand(param => ExecuteLoginClick(param), param => CanExecuteLoginClick(param)));
            }
        }

        private bool CanExecuteLoginClick(object param)
        {
            
        }

        //Check if user is in database. Navigate to main page.
        private void ExecuteLoginClick(object param)
        {
            ScheduleViewModel vm = new ScheduleViewModel();
            Navigation.PushAsync(SchedulePage){ Bindingcontext} ;
        }
    }
}
