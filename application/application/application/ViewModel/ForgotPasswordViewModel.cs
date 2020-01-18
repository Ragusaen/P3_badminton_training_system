using application.UI;
using System;
using System.Collections.Generic;
using System.Text;
using application.SystemInterface;
using Xamarin.Forms;

namespace application.ViewModel
{
    class ForgotPasswordViewModel : BaseViewModel
    {

        public ForgotPasswordViewModel(RequestCreator requestCreator, INavigation navigation) : base(requestCreator,
            navigation)
        {

        }

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
            Navigation.PushAsync(new LoginPage(RequestCreator));
        }
    }
}
