using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Common.Model;
using Xamarin.Forms;

namespace application.ViewModel
{
    class ViewDetailedFeedbackViewModel : BaseViewModel
    {
        private ObservableCollection<Feedback> _feedbacks;

        public ObservableCollection<Feedback> Feedbacks
        {
            get { return _feedbacks; }
            set 
            {
                SetProperty(ref _feedbacks, value);
            }
        }

        public ViewDetailedFeedbackViewModel()
        {
            Feedbacks = new ObservableCollection<Feedback>
            {
                new Feedback
                {
                    PlaySession = new PracticeSession{ Start = DateTime.Now },
                    ReadyQuestion = 2,
                    EffortQuestion = -1,
                    ChallengeQuestion = 0,
                    AbsorbQuestion = 1,
                    GoodQuestion = "",
                    BadQuestion = "",
                    FocusPointQuestion = "",
                    DayQuestion = "",
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

  
       

