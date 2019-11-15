using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace application.ViewModel
{
    class ViewDetailedFeedbackViewModel : BaseViewModel
    { 
        private Feedback _oldFeedback;

        public ObservableCollection<Feedback> Feedbacks { get; set; }

        public ViewDetailedFeedbackViewModel()
        {
            Feedbacks = new ObservableCollection<Feedback>
            {
                new Feedback
                {
                    Name = "Feedback from ",
                    IsVisible = false,
                },
                new Feedback
                {
                    Name = "Feedback from ",
                    IsVisible = false,
                },
                new Feedback
                {
                    Name = "Feedback from ",
                    IsVisible = false,
                },
                new Feedback
                {
                    Name = "Feedback from ",
                    IsVisible = false,
                },
            };
        }

        public void ShowOrHideFeedback(Feedback feedback)
        {
            if (_oldFeedback == feedback)
            {
                // click twice on the same item will hide it
                feedback.IsVisible = !feedback.IsVisible;
                UpdateFeedbacks(feedback);
            }
            else
            {
                if (_oldFeedback != null)
                {
                    // hide previous selected item
                    _oldFeedback.IsVisible = false;
                    UpdateFeedbacks(_oldFeedback);
                }
                // show selected item
                feedback.IsVisible = true;
                UpdateFeedbacks(feedback);
            }

            _oldFeedback = feedback;
        }

        private void UpdateFeedbacks(Feedback feedback)
        {
            var index = Feedbacks.IndexOf(feedback);
            Feedbacks.Remove(feedback);
            Feedbacks.Insert(index, feedback);
        }
    }
}

  
       

