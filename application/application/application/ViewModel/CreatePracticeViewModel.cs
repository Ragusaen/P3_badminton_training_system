using application.UI;
using System;
using System.Collections.Generic;
using System.Text;
using Common.Model;
using System.Collections.ObjectModel;
using System.Linq;
using application.Controller;

namespace application.ViewModel
{
    class CreatePracticeViewModel : BaseViewModel
    {
        private string _practiceTitle;
        public string PracticeTitle
        {
            get { return _practiceTitle; }
            set
            {
                if (SetProperty(ref _practiceTitle, value))
                    SaveCreatedPracticeClickCommand.RaiseCanExecuteChanged();
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
                    SaveCreatedPracticeClickCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _selectedDateEnd;

        public DateTime SelectedDateEnd
        {
            get { return _selectedDateEnd; }
            set
            {
                if (SetProperty(ref _selectedDateEnd, value))
                    SaveCreatedPracticeClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _selectedTimeStart;

        public string SelectedTimeStart
        {
            get { return _selectedTimeStart; }
            set
            {
                if (SetProperty(ref _selectedTimeStart, value))
                    SaveCreatedPracticeClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _selectedTimeEnd;

        public string SelectedTimeEnd
        {
            get { return _selectedTimeEnd; }
            set
            {
                if (SetProperty(ref _selectedTimeEnd, value))
                    SaveCreatedPracticeClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _trainerName;

        public string TrainerName
        {
            get { return _trainerName; }
            set
            {
                if (SetProperty(ref _trainerName, value))
                    SaveCreatedPracticeClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                if (SetProperty(ref _location, value))
                    SaveCreatedPracticeClickCommand.RaiseCanExecuteChanged();
            }
        }

        private List<string> _planElement;
        public List<string> PlanElement
        {
            get { return _planElement; }
            set
            {
                if (SetProperty(ref _planElement, value))
                    SaveCreatedPracticeClickCommand.RaiseCanExecuteChanged();
            }
        }

        
        private RelayCommand _saveCreatedPracticeClickCommand;

        public RelayCommand SaveCreatedPracticeClickCommand
        {
            get
            {
                return _saveCreatedPracticeClickCommand ?? (_saveCreatedPracticeClickCommand = new RelayCommand(param => ExecuteSaveCreatedPracticeClick(param), param => CanExecuteSaveCreatedPracticeClick(param)));
            }
        }

        private bool CanExecuteSaveCreatedPracticeClick(object param)
        {
            if (string.IsNullOrEmpty(PracticeTitle))
                return false;
            else
                return true;
        }

        //Check if username is free in database.
        private void ExecuteSaveCreatedPracticeClick(object param)
        {
            //Navigate back
            Navigation.PopAsync();
        }

        private string _searchtext;

        public string SearchText
        {
            get { return _searchtext; }
            set
            {
                SetProperty(ref _searchtext, value);
                SearchResultFocusPoints = new ObservableCollection<FocusPointItem>(SearchResultFocusPoints.OrderByDescending((x => StringExtension.LongestCommonSubsequence(x.Descriptor.Name, SearchText))).ThenBy(x => x.Descriptor.Name.Length).ToList());
            }
        }

        public ObservableCollection<FocusPointItem> FocusPoint;

        private ObservableCollection<FocusPointItem> _searchResultFocusPoints;

        public ObservableCollection<FocusPointItem> SearchResultFocusPoints
        {
            get { return _searchResultFocusPoints; }
            set
            {
                SetProperty(ref _searchResultFocusPoints, value);
                FocusPointListHeight = SearchResultFocusPoints.Count * 45;
            }
        }

        private int _focusPointListHeight;

        public int FocusPointListHeight
        {
            get { return _focusPointListHeight; }
            set { SetProperty(ref _focusPointListHeight, value); }
        }
        

        private RelayCommand _addNewPlanElementClickCommand;

        public RelayCommand AddNewPlanElementClickCommand
        {
            get
            {
                return _addNewPlanElementClickCommand ?? (_addNewPlanElementClickCommand = new RelayCommand(param => ExecuteAddNewPlanElementClick(param), param => CanExecuteAddNewPlanElementClick(param)));
            }
        }

        private bool CanExecuteAddNewPlanElementClick(object param)
        {
            return true;
        }

        //Check if username is free in database.
        private void ExecuteAddNewPlanElementClick(object param)
        {
            PlanElement.Add("");
        }

        public CreatePracticeViewModel()
        {
            MinDate = DateTime.Today;
            MaxDate = new DateTime(2020, 1, 1);

            FocusPoint = new ObservableCollection<FocusPointItem>();
            SearchResultFocusPoints = FocusPoint;
            //FocusPointsSearchText = focusPoint;

            PlanElement = new List<string>();
            PlanElement.Add("");
            
        }
    }
}
