using application.UI;
using System;
using System.Collections.Generic;
using System.Text;
using Common.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using application.Controller;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using application.SystemInterface;

namespace application.ViewModel
{
    class CreatePracticeViewModel : BaseViewModel
    {
        public PracticeSession Practice { get; set; } = new PracticeSession();

        private bool IsEdit = false;

        //Date

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

        private TimeSpan _selectedTimeStart;

        public TimeSpan SelectedTimeStart
        {
            get { return _selectedTimeStart; }
            set
            {
                if (SetProperty(ref _selectedTimeStart, value))
                    SaveCreatedPracticeClickCommand.RaiseCanExecuteChanged();
                if(SelectedTimeStart > _selectedTimeEnd)
                    SelectedTimeEnd = SelectedTimeStart;
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
                if (SelectedTimeStart > _selectedTimeEnd)
                     SelectedTimeStart = SelectedTimeEnd;
            }
        }

        //List

        private List<Trainer> _trainers;

        public List<Trainer> Trainers
        {
            get { return _trainers; }
            set
            {
                if (SetProperty(ref _trainers, value))
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
        

        private int _focusPointListHeight;
        public int FocusPointListHeight
        {
            get { return _focusPointListHeight; }
            set { SetProperty(ref _focusPointListHeight, value); }
        }
        //rest
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

        private string _teamName;

        public string TeamName
        {
            get => _teamName;
            set
            {
                SetProperty(ref _teamName, value);
            }
        }

        //Ctor
        public CreatePracticeViewModel(DateTime startDate)
        {
            SelectedDateStart = startDate;
            TeamName = "Choose Team";
            FocusPoints = new ObservableCollection<FocusPointItem>();
            Trainers = RequestCreator.GetAllTrainers();
            PlanElement = new ObservableCollection<ExerciseItem>();
        }

        public CreatePracticeViewModel(PracticeSession ps)
        {
            Practice = ps;
            TeamName = ps.PracticeTeam.Name;
            SelectedDateStart = ps.Start;
            SelectedTimeStart = ps.Start.TimeOfDay;
            SelectedTimeEnd = ps.End.TimeOfDay;
            PlanElement = new ObservableCollection<ExerciseItem>(ps.Exercises);
            FocusPoints = new ObservableCollection<FocusPointItem>(ps.FocusPoints);
            if (ps.MainFocusPoint != null)
                FocusPoints.Insert(0, ps.MainFocusPoint);
            FocusPointListHeight = FocusPoints.Count * 45;

            IsEdit = true;
        }

        //Save
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
            return true;
        }

        private void ExecuteSaveCreatedPracticeClick(object param)
        {
            Practice.Start = SelectedDateStart.Date + Convert.ToDateTime(SelectedTimeStart.ToString()).TimeOfDay;
            Practice.End = SelectedDateStart.Date + Convert.ToDateTime(SelectedTimeEnd.ToString()).TimeOfDay;
            if (Practice.PracticeTeam.Name == "Choose Team")
                Practice.PracticeTeam.Name = "";
            if (string.IsNullOrEmpty(Practice.Location))
                Practice.Location = "Stjernevej 5, 9200 Aalborg";
            Practice.Exercises = PlanElement.ToList();

            if (Practice.MainFocusPoint != null)
                FocusPoints?.Remove(Practice.MainFocusPoint);

            Practice.FocusPoints = FocusPoints?.ToList();
            int i = 0;
            foreach (ExerciseItem exerciseitem in Practice.Exercises) 
            {
                exerciseitem.Index = i;
                i++;
            }
            if (IsEdit)
                RequestCreator.DeletePracticeSession(Practice.Id);

            RequestCreator.SetPracticeSession(Practice);
            Navigation.PopAsync();
        }

        public async void AddNewPlanElement(EventHandler<ExerciseDescriptor> eventHandler)
        {
            Debug.WriteLine("SOMWHERE");
            string action = await Application.Current.MainPage.DisplayActionSheet("Settings", "Cancel", null,"Add Existing Exercise", "Make New Exercise");

            if (action == "Add Existing Exercise")
            {
                var page = new ExercisePopupPage(Practice);
                page.CallBackEvent += eventHandler;
                await PopupNavigation.Instance.PushAsync(page);
            }
            else if (action == "Make New Exercise")
                await PopupNavigation.Instance.PushAsync(new CreateExercisePopupPage());
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
            FocusPointPopupPage page = new FocusPointPopupPage(FocusPoints.ToList(), null);
            ((FocusPointPopupViewModel)page.BindingContext).CallBackEvent += FocusPointPage_CallBackEvent;
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
            PracticeTeamPopupPage page = new PracticeTeamPopupPage(new List<PracticeTeam>());
            page.CallBackEvent += TeamPage_CallBackEvent; ;
            PopupNavigation.Instance.PushAsync(page);
        }

        private void TeamPage_CallBackEvent(object sender, PracticeTeam e)
        {
            Practice.PracticeTeam = e;
            TeamName = e.Name;
        }
        //Delete
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


    }
}
