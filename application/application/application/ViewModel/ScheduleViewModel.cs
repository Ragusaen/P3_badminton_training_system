using application.UI;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using application.SystemInterface;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Controls;
using Xamarin.Plugin.Calendar.Models;

namespace application.ViewModel
{
    class ScheduleViewModel : BaseViewModel
    {
        private EventCollection _events = new EventCollection();

        public EventCollection Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
        }

        private int CurrentMonth;

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                SetProperty(ref _selectedDate, value);
                Debug.WriteLine(_selectedDate);
                if (SelectedDate.Month != CurrentMonth)
                {
                    CurrentMonth = SelectedDate.Month;
                    var firstDayOfMonth = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
                    var firstDayOfNextMonth = firstDayOfMonth.AddMonths(1);
                    LoadEvents(firstDayOfMonth, firstDayOfNextMonth);
                }
            }
        }


        public ScheduleViewModel()
        {
            SelectedDate = DateTime.Today;
        }

        public class PlaySessionEvent
        {
            public string Name;
            public string Location;
        }

        private void LoadEvents(DateTime start, DateTime end)
        {
            List<PlaySession> playSessions = RequestCreator.GetSchedule(start, end);

            Debug.WriteLine($"THERE WERE {playSessions.Count} PLAYSESSIONS");

            Events = new EventCollection();

            foreach (PlaySession ps in playSessions)
            {
                if (!Events.ContainsKey(ps.Start))
                    Events.Add(ps.Start, new List<PlaySessionEvent>());
                
                var psEvent = new PlaySessionEvent()
                {
                    Location = ps.Location
                };

                if (ps is PracticeSession practice)
                    psEvent.Name = practice.PracticeTeam.Name;
                else if (ps is TeamMatch tm)
                    psEvent.Name = tm.OpponentName; // Change to team name

                ((List<PlaySessionEvent>)Events[ps.Start]).Add(psEvent);
            }
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
