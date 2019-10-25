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
            MainPageViewModel vm = new MainPageViewModel();
            Navigation.PushAsync(new MainPage() { BindingContext = vm });
            vm.Navigation = Navigation;
        }

        private RelayCommand _forgotPasswordEmailClickCommand;

        public RelayCommand ForgotPasswordEmailClickCommand
        {
            get
            {
                return _forgotPasswordEmailClickCommand ?? (_forgotPasswordEmailClickCommand = new RelayCommand(param => ExecuteForgotPasswordEmailClick(param)));
            }
        }

        //Check if user is in database. Navigate to main page.
        private void ExecuteForgotPasswordEmailClick(object param)
        {
            MainPageViewModel vm = new MainPageViewModel();
            Navigation.PushAsync(new MainPage() { BindingContext = vm });
            vm.Navigation = Navigation;
        }
    }
}
