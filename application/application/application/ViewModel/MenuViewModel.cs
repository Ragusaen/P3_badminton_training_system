using application.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.ViewModel
{
    class MenuViewModel : BaseViewModel
    {
        private RelayCommand _toSchedulePageClickCommand;

        public RelayCommand ToSchedulePageClickCommand
        {
            get
            {
                return _toSchedulePageClickCommand ?? (_toSchedulePageClickCommand = new RelayCommand(param => ExecuteToSchedulePageClick(param)));
            }
        }
        private void ExecuteToSchedulePageClick(object param)
        {
            ScheduleViewModel vm = new ScheduleViewModel();
            Navigation.PushAsync(new SchedulePage() { BindingContext = vm });
            vm.Navigation = Navigation;
        }

        private RelayCommand _toTeamPageClickCommand;

        public RelayCommand ToTeamPageClickCommand
        {
            get
            {
                return _toTeamPageClickCommand ?? (_toTeamPageClickCommand = new RelayCommand(param => ExecuteToTeamPageClick(param)));
            }
        }
        private void ExecuteToTeamPageClick(object param)
        {
            
        }
        private RelayCommand _toProfilePageClickCommand;

        public RelayCommand ToProfilePageClickCommand
        {
            get
            {
                return _toProfilePageClickCommand ?? (_toProfilePageClickCommand = new RelayCommand(param => ExecuteToProfilePageClick(param)));
            }
        }
        private void ExecuteToProfilePageClick(object param)
        {
            ProfilePageViewModel vm = new ProfilePageViewModel();
            Navigation.PushAsync(new ProfilePage() { BindingContext = vm });
            vm.Navigation = Navigation;  
        }
    }
}
