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

  
       

