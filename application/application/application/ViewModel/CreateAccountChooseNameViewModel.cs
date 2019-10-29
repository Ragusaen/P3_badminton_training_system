using System;
using System.Collections.Generic;
using System.Text;

namespace application.ViewModel
{
    class CreateAccountChooseNameViewModel : BaseViewModel
    {
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
            ProfilePageViewModel vm = new ProfilePageViewModel();
            Navigation.PushAsync(new ProfilPage() { BindingContext = vm });
            vm.Navigation = Navigation;
        }
    }
}
