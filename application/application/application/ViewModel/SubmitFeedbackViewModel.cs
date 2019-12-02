using application.SystemInterface;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace application.ViewModel
{
    class SubmitFeedbackViewModel : BaseViewModel
    {
        public Feedback Feedback { get; set; } = new Feedback();

        public SubmitFeedbackViewModel(PlaySession playsession)
        {
            Feedback.PlaySession = playsession;
        }
        private RelayCommand _submitFeedbackCommand;

        public RelayCommand SubmitFeedbackCommand
        {
            get
            {
                return _submitFeedbackCommand ?? (_submitFeedbackCommand = new RelayCommand(param => ExecuteSubmitFeedbackClick(param)));
            }
        }
        private void ExecuteSubmitFeedbackClick(object param)
        {
            if ((Feedback.BadQuestion != null && Feedback.BadQuestion.Length > 1024) || 
                (Feedback.DayQuestion != null && Feedback.DayQuestion.Length > 1024) ||
                (Feedback.FocusPointQuestion != null && Feedback.FocusPointQuestion.Length > 1024) || 
                (Feedback.GoodQuestion != null && Feedback.GoodQuestion.Length > 1024))
            {
                Application.Current.MainPage.DisplayAlert("Invalid input", "Question can not contain more than 1024 characters", "Ok");
                return;
            }
            RequestCreator.SetFeedback(Feedback);
            Navigation.PopAsync();
        }
    }
}
