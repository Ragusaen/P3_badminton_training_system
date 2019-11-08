using System;
using System.Collections.Generic;
using System.Text;

namespace application.ViewModel
{
    class SubmitFeedbackViewModel : BaseViewModel
    {
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

        }
    }
}
