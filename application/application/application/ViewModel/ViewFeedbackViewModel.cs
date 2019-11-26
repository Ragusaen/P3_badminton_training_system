using System;
using System.Collections.Generic;
using System.Text;
using Entry = Microcharts.Entry;
using Microcharts;
using SkiaSharp;
using application.SystemInterface;
using Common.Model;

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
        public ViewFeedbackViewModel(Player player) 
        {
            List<Feedback> feedbacks = RequestCreator.GetPlayerFeedback(player.Member);
            List<Entry> entries = new List<Entry>();
            List<Entry> entries1 = new List<Entry>();
            List<Entry> entries2 = new List<Entry>();
            List<Entry> entries3 = new List<Entry>();
            //fix feedback submit date
            foreach (Feedback fb in feedbacks) 
            { 
                entries.Add(new Entry((float)fb.ReadyQuestion) { Color = SKColor.Parse("#33ccff"), Label = fb.PlaySession.Start.Date.ToString(), ValueLabel = FeedbackString(fb.ReadyQuestion) });
                entries1.Add(new Entry((float)fb.EffortQuestion) { Color = SKColor.Parse("#33ccff"), Label = fb.PlaySession.Start.Date.ToString(), ValueLabel = FeedbackString(fb.EffortQuestion) });
                entries2.Add(new Entry((float)fb.ChallengeQuestion) { Color = SKColor.Parse("#33ccff"), Label = fb.PlaySession.Start.Date.ToString(), ValueLabel = FeedbackString(fb.ChallengeQuestion) });
                entries3.Add(new Entry((float)fb.AbsorbQuestion) { Color = SKColor.Parse("#33ccff"), Label = fb.PlaySession.Start.Date.ToString(), ValueLabel =  FeedbackString(fb.AbsorbQuestion)});  
            }
  
            Chart = new LineChart { Entries = entries, LineMode = LineMode.Straight, PointMode = PointMode.Circle, LabelTextSize = 25, PointSize = 12 };
            Chart1 = new LineChart { Entries = entries1, LineMode = LineMode.Straight, PointMode = PointMode.Square, LabelTextSize = 25, PointSize = 12 };
            Chart2 = new LineChart { Entries = entries2, LineMode = LineMode.Straight, PointMode = PointMode.Square, LabelTextSize = 25, PointSize = 12 };
            Chart3 = new LineChart { Entries = entries3, LineMode = LineMode.Straight, PointMode = PointMode.Square, LabelTextSize = 25, PointSize = 12 };
        }
        public string FeedbackString(int value)
        {
            if (value == 2)
                return "best";
            else if (value == 1)
                return "good";
            else if (value == 0)
                return "average";
            else if (value == -1)
                return "bad";
            else if (value == -2)
                return "worst";
            else
                return null;
        }

    }
}
