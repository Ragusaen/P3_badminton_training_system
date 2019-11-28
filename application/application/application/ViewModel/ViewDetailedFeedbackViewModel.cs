using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Common.Model;
using Xamarin.Forms;
using application.SystemInterface;
using System.Linq;

namespace application.ViewModel
{
    class FB : Feedback 
    {
        
        public string Label1 { get; set; }
        public string Label2 { get; set; }
        public string Label3 { get; set; }
        public string Label4 { get; set; }
        public string Label5 { get; set; }
        public string Label6 { get; set; }
        public string Label7 { get; set; }
        public string Label8 { get; set; }
        public FB(Feedback feedback)
        {
            base.AbsorbQuestion = feedback.AbsorbQuestion;
            base.BadQuestion = feedback.BadQuestion;
            base.ChallengeQuestion = feedback.ChallengeQuestion;
            base.DayQuestion = feedback.DayQuestion;
            base.EffortQuestion = feedback.EffortQuestion;
            base.FocusPointQuestion = feedback.FocusPointQuestion;
            base.GoodQuestion = feedback.GoodQuestion;
            base.ReadyQuestion = feedback.ReadyQuestion;
            base.Player = feedback.Player;
            base.PlaySession = feedback.PlaySession;
        }
    }
    class ViewDetailedFeedbackViewModel : BaseViewModel
    {
        public Player Player { get; set; }


        private ObservableCollection<FB> _feedbacks;

        public ObservableCollection<FB> Feedbacks
        {
            get { return _feedbacks; }
            set 
            {
                SetProperty(ref _feedbacks, value);
            }
        }

        public ViewDetailedFeedbackViewModel(Player player)
        {
            Player = player;
            Player.Feedbacks = RequestCreator.GetPlayerFeedback(Player.Member);
            Feedbacks = new ObservableCollection<FB>();


            foreach (Feedback fb in Player.Feedbacks)
            {
                if (fb.PlaySession is PracticeSession)
                    Feedbacks.Add(new FB(fb) 
                    {
                        Label1 = "How ready did you feel to train today?",
                Label2 = "How was your effort today taking into account how ready you felt?",
                Label3 = "How were you challenged today in relation to the exercises?",
                Label4 = "To what extent were you absorbed by the training today?",
                Label5 = "What helped make the training good today?",
                Label6 = "Were there any issues with the training today?",
                Label7 = "What were the main focus points for you today?",
                Label8 = "How has your day been today?"
            });
                if (fb.PlaySession is TeamMatch)
                    Feedbacks.Add(new FB(fb)
                    {
                        Label1 = "How ready did you feel play a match today?",
                Label2 = "How was your effort today taking into account how ready you felt?",
                Label3 = "How were you challenged today?",
                Label4 = "To what extent were you absorbed by the match today?",
                Label5 = "What helped make the match good today?",
                Label6 = "Were there any issues with the match today?",
                Label7 = "What were the main focus points for you today?",
                Label8 = "How has your day been today?"
            });
            Feedbacks = new ObservableCollection<FB>(Feedbacks.OrderByDescending(p => p.PlaySession.Start.Date).ThenByDescending(p => p.PlaySession.Start.TimeOfDay).ToList());

            }
        }

        private RelayCommand _expandCommand;

        public RelayCommand ExpandCommand
        {
            get
            {
                return _expandCommand ?? (_expandCommand = new RelayCommand(param => ExpandClick(param)));
            }
        }
        private void ExpandClick(object param)
        {
            var Layout = param as StackLayout;
            if (Layout.IsVisible == true)
                Layout.IsVisible = false;
            else
                Layout.IsVisible = true;
        }
    }
}

  
       

