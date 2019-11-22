using application.UI;
using System;
using System.Collections.Generic;
using System.Text;
using Common.Model;
using System.Collections.ObjectModel;
using System.Linq;
using application.Controller;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using application.SystemInterface;

namespace application.ViewModel
{
    class CreatePracticeViewModel : BaseViewModel
    {
        YearPlanSection YearPlan { get; set; } = new YearPlanSection();
        
        public PracticeSession Practice = new PracticeSession();
        private string _practiceTitle;
        public string PracticeTitle
        {
            get => _practiceTitle;
            set
            {
                if (SetProperty(ref _practiceTitle, value))
                    SaveCreatedPracticeClickCommand.RaiseCanExecuteChanged();
            }
        }
        private int _planHeight;
        public int PlanHeight
        {
            get => _planHeight;
            set => SetProperty(ref _planHeight, value);
        }

        public DateTime MinDate { get; set; } = DateTime.Today;

        private DateTime _selectedDateStart;

        public DateTime SelectedDateStart
        {
            get => _selectedDateStart;
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
                FocusPoints = new ObservableCollection<FocusPointItem>(FocusPoints.OrderByDescending((x => StringExtension.LongestCommonSubsequence(x.Descriptor.Name, SearchText))).ThenBy(x => x.Descriptor.Name.Length).ToList());
            }
        }

        private ObservableCollection<FocusPointItem> _focusPoints;

        public ObservableCollection<FocusPointItem> FocusPoints
        {
            get { return _focusPoints; }
            set
            {
                SetProperty(ref _focusPoints, value);
                FocusPointListHeight = FocusPoints.Count * 45;
            }
        }
        private ObservableCollection<ExerciseItem> _exercise;

        public ObservableCollection<ExerciseItem> Exercise
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
        private async void ExecuteAddNewPlanElementClick(object param)
        {
            string action = await Application.Current.MainPage.DisplayActionSheet("Settings", "Cancel", null,"Add Existing Exercise", "Make New Exercise");

            if (action == "Add Existing Exercise")
            {
                var page = new ExercisePopupPage(Practice);
                page.CallBackEvent += ExercisePage_CallBackEvent;
                await PopupNavigation.Instance.PushAsync(page);
            }
            else if (action == "Make New Exercise")
                await PopupNavigation.Instance.PushAsync(new CreateExercisePopupPage());
        }

        private void ExercisePage_CallBackEvent(object sender, ExerciseDescriptor e)
        {
            ExerciseItem item = new ExerciseItem() { ExerciseDescriptor = e };
            PlanElement.Add(item);
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
        private RelayCommand _deleteFocusPointItemCommand;

        public RelayCommand DeleteFocusPointItemCommand
        {
            get
            {
                return _deleteFocusPointItemCommand ?? (_deleteFocusPointItemCommand = new RelayCommand(param => DeleteFocusPointItemClick(param)));
            }
        }
        private void DeleteFocusPointItemClick(object param)
        {
            FocusPointItem exercise = param as FocusPointItem;
            FocusPoints.Remove(exercise);
            FocusPointListHeight = FocusPoints.Count * 45;
        }

        public CreatePracticeViewModel()
        {
            SelectedDateStart = DateTime.Today;
            FocusPoints = new ObservableCollection<FocusPointItem>();

            PlanElement = new ObservableCollection<ExerciseItem>();
            PlanHeight = PlanElement.Count * 235;
        }
        private RelayCommand _addNewFocusPointCommand;

        public RelayCommand AddNewFocusPointCommand
        {
            get
            {
                return _addNewFocusPointCommand ?? (_addNewFocusPointCommand = new RelayCommand(param => AddNewFocusPointClick(param)));
            }
        }
        private void AddNewFocusPointClick(object param)
        {
            FocusPointPopupPage page = new FocusPointPopupPage(FocusPoints.ToList());
            page.CallBackEvent += FocusPointPage_CallBackEvent; ;
            PopupNavigation.Instance.PushAsync(page);
        }

        private void FocusPointPage_CallBackEvent(object sender, FocusPointDescriptor e)
        {
            FocusPoints.Add(new FocusPointItem() { Descriptor = e });
            FocusPointListHeight = FocusPoints.Count * 45;
        }
        private RelayCommand _teamCommand;

        public RelayCommand TeamCommand
        {
            get
            {
                return _teamCommand ?? (_teamCommand = new RelayCommand(param => TeamClick(param)));
            }
        }
        private void TeamClick(object param)
        {
            FocusPointPopupPage page = new FocusPointPopupPage(FocusPoints.ToList());
            page.CallBackEvent += FocusPointPage_CallBackEvent; ;
            PopupNavigation.Instance.PushAsync(page);
        }
    }
}
