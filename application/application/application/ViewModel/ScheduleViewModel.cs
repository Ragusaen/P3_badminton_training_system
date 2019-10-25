using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace application.ViewModel
{
    class ScheduleViewModel : BaseViewModel
    {
     
        private string _currentMonth;

        public string CurrentMonth
        {
            get { return _currentMonth; }
            set
            {
                if (SetProperty(ref _currentMonth, value))
                    DateClickCommand.RaiseCanExecuteChanged();
            }
        }

        public ScheduleViewModel()
        {
            CurrentMonth = DateTime.Today.ToString("MMMM");
        }

        private RelayCommand _dateClickCommand;

        public RelayCommand DateClickCommand
        {
            get
            {
                return _dateClickCommand ?? (_dateClickCommand = new RelayCommand(param => ExecuteDateClick(param), param => CanExecuteDateClick(param)));
            }
        }

        private bool CanExecuteDateClick(object param)
        {
            
                return true;
        }

        //Check if user is in database. Navigate to main page.
        private void ExecuteDateClick(object param)
        {
           
        }
    }
}
