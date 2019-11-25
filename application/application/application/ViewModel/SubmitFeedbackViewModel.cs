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


        public SubmitFeedbackViewModel(PlaySession playsession)
        {
            Feedback.PlaySession = playsession;
            Feedback.Player = new Player { Member = new Member { Id = 123 } };
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
            RequestCreator.SetFeedback(Feedback);
        }
    }
}
