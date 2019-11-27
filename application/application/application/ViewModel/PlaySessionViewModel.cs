﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using application.UI;
using Common.Model;
using Common.Serialization;

namespace application.ViewModel
{
    class PlaySessionViewModel : BaseViewModel
    {
        public PlaySession PlaySession { get; set; }
        public PracticeSession PracticeSession { get; set; }
        public TeamMatch TeamMatch { get; set; }

        private bool _practiceFeedbackIsVisible;

        public bool PracticeFeedbackIsVisible
        {
            get => _practiceFeedbackIsVisible;
            set => SetProperty(ref _practiceFeedbackIsVisible, value);
        }

        private double _focusPointListHeight;
        public double FocusPointListHeight
        {
            get => _focusPointListHeight;
            set => SetProperty(ref _focusPointListHeight, value);
        }



        public PlaySessionViewModel(PlaySession playSession)
        {
            PlaySession = playSession;

            if (PlaySession.Start < DateTime.Now && DateTime.Now < PlaySession.Start.AddDays(7))
                PracticeFeedbackIsVisible = true;

            if (PlaySession is PracticeSession practice)
            {
                PracticeSession = practice;
                _focusPointListHeight = PracticeSession.FocusPoints.Count * 45;
            }
            else if (PlaySession is TeamMatch match)
                TeamMatch = match;
        }
        private RelayCommand _feedbackCommand;

        public RelayCommand FeedbackCommand
        {
            get
            {
                return _feedbackCommand ?? (_feedbackCommand = new RelayCommand(param => FeedbackClick(param)));
            }
        }

        private void FeedbackClick(object param)
        {
            Navigation.PushAsync(new SubmitFeedbackPage(PlaySession));
        }
    }
}
