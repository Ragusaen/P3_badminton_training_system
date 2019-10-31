using application.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.ViewModel
{
    class ForgotPasswordViewModel : BaseViewModel
    {
        private RelayCommand _backToLogInClickCommand;

        public RelayCommand BackToLogInClickCommand
        {
            get
            {
                return _backToLogInClickCommand ?? (_backToLogInClickCommand = new RelayCommand(param => ExecuteBackToLogInClick(param)));
            }
        }

        //Check if user is in database. Navigate to main page.
        private void ExecuteBackToLogInClick(object param)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private RelayCommand _sendEmailClickCommand;

        public RelayCommand SendEmailClickCommand
        {
            get
            {
                return _sendEmailClickCommand ?? (_sendEmailClickCommand = new RelayCommand(param => ExecuteSendEmailClick(param)));
            }
        }

        //Check if user is in database. Navigate to main page.
        private void ExecuteSendEmailClick(object param)
        {
            
        }
    }
}
