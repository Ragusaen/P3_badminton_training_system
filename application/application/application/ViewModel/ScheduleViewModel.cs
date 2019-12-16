using application.UI;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using application.Controller;
using application.SystemInterface;
using application.UI.Converter;
using Common.Serialization;
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

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                SetProperty(ref _selectedDate, value);
                SetSelectedEvents();
            }
        }
        public bool IsTrainer { get; set; }

        public ScheduleViewModel()
        {
            IsTrainer = ((RequestCreator.LoggedInMember.MemberType & MemberType.Trainer) == MemberType.Trainer);
            SelectedDate = DateTime.Today;
            _month = DateTime.Today.Month;
            _year = DateTime.Today.Year;
            LoadEvents();
            SetSelectedEvents();
        }

        private ObservableCollection<PlaySessionEvent> _selectedEvents;
        public ObservableCollection<PlaySessionEvent> SelectedEvents
        {
            get => _selectedEvents;
            set => SetProperty(ref _selectedEvents, value);
        }
        private void SetSelectedEvents()
        {
            if (Events.ContainsKey(SelectedDate))
                SelectedEvents = new ObservableCollection<PlaySessionEvent>(((List<PlaySessionEvent>)Events[SelectedDate]).OrderBy(p => p.Time));
            else
                SelectedEvents = new ObservableCollection<PlaySessionEvent>();
        }

        public class PlaySessionEvent
        {
            public string Name { get; set; }
            public string Location { get; set; }
            public string Time { get; set; }
            public string Detail { get; set; }
            public Color Color { get; set; }
            public PlaySession PlaySession;
        }

        private bool _relevantOnly = false;
        public bool RelevantOnly
        {
            get => _relevantOnly;
            set
            {
                _relevantOnly = value;
                Events = new EventCollection();
                LoadEvents();
                SetSelectedEvents();
            }
        }

        private void LoadEvents()
        {
            DateTime start = new DateTime(_year, _month, 1);
            DateTime end = start.AddMonths(1);

            var scheduleData = RequestCreator.GetSchedule(start, end);

            List<PlaySession> playSessions = scheduleData.playSessions;
            List<bool> isMemberRelevant = scheduleData.relevance;

            for (int i = 0; i < playSessions.Count; i++)
            {
                var ps = playSessions[i];

                if (RelevantOnly && !isMemberRelevant[i])
                    continue;

                // Add entry to dictionary if it doesn't exist
                if (!Events.ContainsKey(ps.Start.Date))
                    Events.Add(ps.Start.Date, new List<PlaySessionEvent>());
                // Skip if the event already has been added
                else if (((List<PlaySessionEvent>) Events[ps.Start]).Any(pse => pse.PlaySession.Id == ps.Id))
                    continue;
                
                var psEvent = new PlaySessionEvent()
                {
                    Location = ps.Location,
                    Time = ps.Start.ToString("HH:mm"),
                    PlaySession = ps
                };

                if (ps is PracticeSession practice)
                {
                    psEvent.Name = practice.PracticeTeam.Name;
                    psEvent.Detail = practice.Trainer == null ? null : practice.Trainer.Member.Name;
                    psEvent.Color = Color.DarkSeaGreen;
                }
                else if (ps is TeamMatch tm)
                {
                    string leagueString = Enum.GetName(typeof(TeamMatch.Leagues), (TeamMatch.Leagues) tm.League);
                    psEvent.Name = StringExtension.SplitCamelCase(leagueString);
                    psEvent.Detail = tm.OpponentName;
                    psEvent.Color = Color.CornflowerBlue;
                }

                ((List<PlaySessionEvent>)Events[ps.Start]).Add(psEvent);
            }
        }
    }
}
