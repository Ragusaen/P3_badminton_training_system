using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Common.Model;
using Xamarin.Forms;
using application.SystemInterface;

namespace application.ViewModel
{
    class ViewDetailedFeedbackViewModel : BaseViewModel
    {
        public Player Player { get; set; }

        private ObservableCollection<Feedback> _feedbacks;

        public ObservableCollection<Feedback> Feedbacks
        {
            get { return _feedbacks; }
            set 
            {
                SetProperty(ref _feedbacks, value);
            }
        }

        public ViewDetailedFeedbackViewModel(Member member)
        {
            Player.Feedbacks = RequestCreator.GetPlayerFeedback();
            Player.Member = member;

            Player = new Player { Member = new Member { Name = "Mig", },
                Feedbacks = new List<Feedback>
                {
                    new Feedback
                    {
                        PlaySession = new PracticeSession{ Start = DateTime.Now },
                        ReadyQuestion = 2,
                        EffortQuestion = -1,
                        ChallengeQuestion = 0,
                        AbsorbQuestion = 1,
                        GoodQuestion = "hej",
                        BadQuestion = "med",
                        FocusPointQuestion = "jer",
                        DayQuestion = "2",
                    },
                    new Feedback
                    {
                        PlaySession = new PracticeSession{ Start = DateTime.Now },
                    },
                    new Feedback
                    {
                        PlaySession = new PracticeSession{ Start = DateTime.Now },
                    },
                    new Feedback
                    {
                        PlaySession = new PracticeSession{ Start = DateTime.Now },
                    },
                } 
            };
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

  
       

