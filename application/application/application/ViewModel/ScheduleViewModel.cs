using application.UI;
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

        private bool _visibility;

        public bool Visibility
        {
            get { return _visibility; }
            set
            {
                  if (SetProperty(ref _visibility, value))
                    AddClickCommand.RaiseCanExecuteChanged();
            }
        }

        public ScheduleViewModel()
        {
            CurrentMonth = DateTime.Today.ToString("MMMM");
            Visibility = false;
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

        private void ExecuteDateClick(object param)
        {
           
        }

        private RelayCommand _addClickCommand;

        public RelayCommand AddClickCommand
        {
            get
            {
                return _addClickCommand ?? (_addClickCommand = new RelayCommand(param => ExecuteAddClick(param), param => CanExecuteAddClick(param)));
            }
        }

        private bool CanExecuteAddClick(object param)
        {
           return true;
        }

      
        private void ExecuteAddClick(object param)
        {
            if (Visibility == true)
                Visibility = false;
            else if (Visibility == false)
                Visibility = true;
        }

        private RelayCommand _addNewPracticeClickCommand;

        public RelayCommand AddNewPracticeClickCommand
        {
            get
            {
                return _addNewPracticeClickCommand ?? (_addNewPracticeClickCommand = new RelayCommand(param => ExecuteAddNewPracticeClick(param), param => CanExecuteAddNewPracticeClick(param)));
            }
        }

        private bool CanExecuteAddNewPracticeClick(object param)
        {
            return true;
        }
        
        private void ExecuteAddNewPracticeClick(object param)
        {
            Navigation.PushAsync(new CreatePracticePage());
        }

        private RelayCommand _addNewMatchClickCommand;

        public RelayCommand AddNewMatchClickCommand
        {
            get
            {
                return _addNewMatchClickCommand ?? (_addNewMatchClickCommand = new RelayCommand(param => ExecuteAddNewMatchClick(param), param => CanExecuteAddNewMatchClick(param)));
            }
        }

        private bool CanExecuteAddNewMatchClick(object param)
        {
            return true;
        }

        private void ExecuteAddNewMatchClick(object param)
        {
            Navigation.PushAsync(new CreateMatchPage());
        }
    }
}
