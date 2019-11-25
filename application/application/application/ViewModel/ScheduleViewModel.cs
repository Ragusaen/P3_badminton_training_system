using application.UI;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        private int _month;
        public int Month
        {
            get => _month;
            set
            {
                SetProperty(ref _month, value);
                LoadEvents();
            }
        }

        private int _year;

        public int Year
        {
            get => _year;
            set
            {
                SetProperty(ref _year, value);
                LoadEvents();
            }
        }



        private int CurrentMonth;

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }


        public ScheduleViewModel()
        {
            SelectedDate = DateTime.Today;
            _month = DateTime.Today.Month;
            _year = DateTime.Today.Year;
        }

        public class PlaySessionEvent
        {
            public string Name;
            public string Location;
        }

        private void LoadEvents()
        {
            DateTime start = new DateTime(_year, _month, 1);
            DateTime end = start.AddMonths(1);

            List<PlaySession> playSessions = RequestCreator.GetSchedule(start, end);

            Debug.WriteLine($"THERE WERE {playSessions.Count} PLAYSESSIONS");

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

            Debug.WriteLine(Events.Count);
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

        private RelayCommand _command;

        public RelayCommand command
        {
            get
            {
                return _command ?? (_command = new RelayCommand(param => Click(param)));
            }
        }
        PlaySession play = new PracticeSession();
        private void Click(object param)
        {
            Navigation.PushAsync(new PlaySessionPage(play));
        }
    }
}
