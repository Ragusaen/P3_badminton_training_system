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
        YearPlanSection YearPlan { get; set; } = new YearPlanSection();
        
        public PracticeSession Practice = new PracticeSession();
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
        private int _planHeight;
        public int PlanHeight
        {
            get { return _planHeight; }
            set
            {
                if (SetProperty(ref _planHeight, value)) ;
            }
        }

        public DateTime MinDate { get; set; } = DateTime.Today;

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

        private TimeSpan _selectedTimeStart;

        public TimeSpan SelectedTimeStart
        {
            get { return _selectedTimeStart; }
            set
            {
                if (SetProperty(ref _selectedTimeStart, value))
                    SaveCreatedPracticeClickCommand.RaiseCanExecuteChanged();
            }
        }

        private TimeSpan _selectedTimeEnd;

        public TimeSpan SelectedTimeEnd
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

        private ObservableCollection<ExerciseItem> _planElement;
        public ObservableCollection<ExerciseItem> PlanElement
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

        private void ExecuteSaveCreatedPracticeClick(object param)
        {
            Practice.Start = SelectedDateStart.Date + Convert.ToDateTime(SelectedTimeStart.ToString()).TimeOfDay;
            Practice.End = SelectedDateEnd.Date + Convert.ToDateTime(SelectedTimeEnd.ToString()).TimeOfDay;
            //Practice.
        }

        private string _searchtext;

        public string SearchText
        {
            get { return _searchtext; }
            set
            {
                SetProperty(ref _searchtext, value);
                FocusPoints = new ObservableCollection<FocusPointDescriptor>(FocusPoints.OrderByDescending((x => StringSearch.LongestCommonSubsequence(x.Name, SearchText))).ThenBy(x => x.Name.Length).ToList());
            }
        }

        private ObservableCollection<FocusPointDescriptor> _focusPoints;

        public ObservableCollection<FocusPointDescriptor> FocusPoints
        {
            get { return _focusPoints; }
            set
            {
                SetProperty(ref _focusPoints, value);
                FocusPointListHeight = FocusPoints.Count * 45;
            }
        }
        private ObservableCollection<ExerciseDescriptor> _exercise;

        public ObservableCollection<ExerciseDescriptor> Exercise
        {
            get { return _exercise; }
            set
            {
                SetProperty(ref _exercise, value);
                FocusPointListHeight = FocusPoints.Count * 45;
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
            PlanElement.Add(new ExerciseItem());
            PlanHeight = PlanElement.Count * 235;
        }
        
        private RelayCommand _deletePlanItemCommand;

        public RelayCommand DeletePlanItemCommand
        {
            get
            {
                return _deletePlanItemCommand ?? (_deletePlanItemCommand = new RelayCommand(param => DeletePlanItemClick(param)));
            }
        }
        private void DeletePlanItemClick(object param)
        {
            ExerciseItem exercise = param as ExerciseItem;
            PlanElement.Remove(exercise);
            PlanHeight = PlanElement.Count * 235;
        }

        public CreatePracticeViewModel()
        {
            SelectedDateStart = DateTime.Today;

            PlanElement = new ObservableCollection<ExerciseItem>();
            PlanElement.Add(new ExerciseItem());
            PlanHeight = PlanElement.Count * 235;

        }
    }
}
