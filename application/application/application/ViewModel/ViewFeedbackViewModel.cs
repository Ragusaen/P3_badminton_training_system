﻿using System;
using System.Collections.Generic;
using System.Text;
using Entry = Microcharts.Entry;
using Microcharts;
using SkiaSharp;
using application.SystemInterface;
using Common.Model;
using System.Linq;
using Xamarin.Forms;

namespace application.ViewModel
{
    class ViewFeedbackViewModel : BaseViewModel
    {
        private Chart _chart;

        public Chart Chart
        {
            get { return _chart; }
            set
            {
                SetProperty(ref _chart, value);
            }
        }
        private Chart _chart1;

        public Chart Chart1
        {
            get { return _chart1; }
            set
            {
                SetProperty(ref _chart1, value);
            }
        }

        private Chart _chart2;

        public Chart Chart2
        {
            get { return _chart2; }
            set
            {
                SetProperty(ref _chart2, value);
            }
        }
        private Chart _chart3;

        public Chart Chart3
        {
            get { return _chart3; }
            set
            {
                SetProperty(ref _chart3, value);
            }
        }
        public ViewFeedbackViewModel(Player player, RequestCreator requestCreator, INavigation navigation) : base(requestCreator, navigation)
        {
            List<Feedback> feedbacks = RequestCreator.GetPlayerFeedback(player.Member);
            feedbacks = feedbacks.OrderByDescending(p => p.PlaySession.Start.Date).ThenByDescending(p => p.PlaySession.Start.TimeOfDay).ToList();
            List<Entry> entries = new List<Entry>();
            List<Entry> entries1 = new List<Entry>();
            List<Entry> entries2 = new List<Entry>();
            List<Entry> entries3 = new List<Entry>();

            int i = 0;
            foreach (Feedback fb in feedbacks) 
            {
                if (i == 15)
                    break;
                entries.Add(new Entry((float)fb.ReadyQuestion) { Color = SKColor.Parse("#33ccff"), ValueLabel = fb.PlaySession.Start.Date.ToString("dd/MM-yyyy") });
                entries1.Add(new Entry((float)fb.EffortQuestion) { Color = SKColor.Parse("#33ccff"), ValueLabel = fb.PlaySession.Start.Date.ToString("dd/MM-yyyy") });
                entries2.Add(new Entry((float)fb.ChallengeQuestion) { Color = SKColor.Parse("#33ccff"), ValueLabel = fb.PlaySession.Start.Date.ToString("dd/MM-yyyy") });
                entries3.Add(new Entry((float)fb.AbsorbQuestion) { Color = SKColor.Parse("#33ccff"), ValueLabel = fb.PlaySession.Start.Date.ToString("dd/MM-yyyy") });
                i++;
                
            }
            entries.Reverse();
            entries1.Reverse();
            entries2.Reverse();
            entries3.Reverse();

            Chart = new LineChart { Entries = entries, LineMode = LineMode.Straight, PointMode = PointMode.Circle, LabelTextSize = 25, PointSize = 12, MaxValue = 2, MinValue = -2 };
            Chart1 = new LineChart { Entries = entries1, LineMode = LineMode.Straight, PointMode = PointMode.Square, LabelTextSize = 25, PointSize = 12, MaxValue = 2, MinValue = -2 };
            Chart2 = new LineChart { Entries = entries2, LineMode = LineMode.Straight, PointMode = PointMode.Square, LabelTextSize = 25, PointSize = 12, MaxValue = 2, MinValue = -2 };
            Chart3 = new LineChart { Entries = entries3, LineMode = LineMode.Straight, PointMode = PointMode.Square, LabelTextSize = 25, PointSize = 12, MaxValue = 2, MinValue = -2 };
        }
    }
}
