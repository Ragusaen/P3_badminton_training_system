using application.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.ViewModel
{
    class CreateMatchViewModel : BaseViewModel
    {
        private string _teamName;
        public string TeamName
        {
            get { return _teamName; }
            set
            {
                if (SetProperty(ref _teamName, value))
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _minDate;

        public DateTime MinDate
        {
            get { return _minDate; }
            set
            {
                SetProperty(ref _minDate, value);
            }
        }

        private DateTime _maxDate;

        public DateTime MaxDate
        {
            get { return _maxDate; }
            set
            {
                SetProperty(ref _maxDate, value);
            }
        }

        private DateTime _selectedDateStart;

        public DateTime SelectedDateStart
        {
            get { return _selectedDateStart; }
            set
            {
                if (SetProperty(ref _selectedDateStart, value))
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _selectedDateEnd;

        public DateTime SelectedDateEnd
        {
            get { return _selectedDateEnd; }
            set
            {
                if (SetProperty(ref _selectedDateEnd, value))
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _selectedTimeStart;

        public string SelectedTimeStart
        {
            get { return _selectedTimeStart; }
            set
            {
                if (SetProperty(ref _selectedTimeStart, value))
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _selectedTimeEnd;

        public string SelectedTimeEnd
        {
            get { return _selectedTimeEnd; }
            set
            {
                if (SetProperty(ref _selectedTimeEnd, value))
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _matchResponsibleName;

        public string MatchResponsibleName
        {
            get { return _matchResponsibleName; }
            set
            {
                if (SetProperty(ref _matchResponsibleName, value))
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                if (SetProperty(ref _location, value))
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
            }
        }

        private RelayCommand _saveMatchClickCommand;

        public RelayCommand SaveMatchClickCommand
        {
            get
            {
                return _saveMatchClickCommand ?? (_saveMatchClickCommand = new RelayCommand(param => ExecuteSaveMatchClick(param), param => CanExecuteSaveMatchClick(param)));
            }
        }

        private bool CanExecuteSaveMatchClick(object param)
        {
            if ((TeamName == null || TeamName == "") || (SelectedDateStart == null) || (SelectedDateEnd == null) || (SelectedTimeStart == null) || (SelectedTimeEnd == null))
                return false;
            else
                return true;
        }

        //Check if username is free in database.
        private void ExecuteSaveMatchClick(object param)
        {
            ScheduleViewModel vm = new ScheduleViewModel();
            Navigation.PushAsync(new SchedulePage() { BindingContext = vm });
            vm.Navigation = Navigation;
        }
    }
}
