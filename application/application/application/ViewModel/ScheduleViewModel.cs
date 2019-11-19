using application.UI;
using Common.Model;
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

        public ScheduleViewModel(Member member)
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

      
        private async void ExecuteAddClick(object param)
        {
            string action = await Application.Current.MainPage.DisplayActionSheet("Choose what you want to add:", "Cancel", null, "Add New Practice", "Add New Match");

            if (action == "Add New Practice")
                await Navigation.PushAsync(new CreatePracticePage());
            else if (action == "Add New Match")
                await Navigation.PushAsync(new CreateMatchPage());
        }

        
    }
}
