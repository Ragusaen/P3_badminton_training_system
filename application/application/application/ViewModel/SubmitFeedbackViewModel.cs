using application.SystemInterface;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.ViewModel
{
    class SubmitFeedbackViewModel : BaseViewModel
    {
        public Feedback Feedback { get; set; } = new Feedback();

        PlaySession PlaySession;


        public SubmitFeedbackViewModel(PlaySession playsession)
        {
            PlaySession = playsession;       
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
            Feedback.PlaySession = new PracticeSession() {Id = 1};
            RequestCreator.SetFeedback(Feedback);
        }
    }
}
