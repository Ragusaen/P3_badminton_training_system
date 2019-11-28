﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using application.Controller;
using application.SystemInterface;
using application.UI;
using Common.Model;
using Common.Serialization;
using Xamarin.Forms;

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
            bool hasNotFeedbacked = true;
            List<Feedback> feedbacks = RequestCreator.GetPlayerFeedback(RequestCreator.LoggedInMember);
            foreach (Feedback fb in feedbacks) 
            {
                if (fb.PlaySession.Id == playSession.Id)
                    hasNotFeedbacked = false;
            }

            PlaySession = playSession;
            DateTime feedbackexdate = PlaySession.Start.AddDays(7);
            if (DateTime.Compare(PlaySession.Start, DateTime.Now) <= 0 && DateTime.Compare(DateTime.Now, feedbackexdate) <= 0 && hasNotFeedbacked)
                PracticeFeedbackIsVisible = true;
            else
                PracticeFeedbackIsVisible = false;

            if (PlaySession is PracticeSession practice)
            {
                PracticeSession = practice;
                _focusPointListHeight = PracticeSession.FocusPoints.Count * 45;
            }
            else if (PlaySession is TeamMatch match)
            {
                TeamMatch = match;
                SetLineupTemplate(match.League);
                SetPositionsFromLineup(match.Lineup);
            }
        }

        private void SetPositionsFromLineup(Lineup lineup)
        {
            foreach (var group in lineup)
            {
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    Positions[(group.Type, i)] = group.Positions[i];
                }
            }
        }

        private void SetLineupTemplate(TeamMatch.Leagues value)
        {
            var positions = new Dictionary<(Lineup.PositionType, int), Position>();
            var template = Lineup.LeaguePositions[value];
            foreach (var group in template)
            {
                for (int i = 0; i < group.Value; i++)
                {
                    positions.Add((group.Key, i), new Position());
                }
            }
            Positions = positions;
        }

        private Dictionary<(Lineup.PositionType, int), Position> _positions;

        public Dictionary<(Lineup.PositionType, int), Position> Positions
        {
            get { return _positions; }
            set
            {
                if (SetProperty(ref _positions, value))
                    LineupHeight = _positions.Count * 110;
            }
        }

        private int _lineupHeight;

        public int LineupHeight
        {
            get { return _lineupHeight; }
            set { SetProperty(ref _lineupHeight, value); }
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

        public async void EditButtonClicked(Page currentPage)
        {
            string edit = await Application.Current.MainPage.DisplayActionSheet("Options", "Cancel", null, "Edit", "Delete");

            if (edit == "Edit")
            {
                Page page;
                if (PracticeSession != null)
                    page = new CreatePracticePage(PracticeSession);
                else if (TeamMatch != null)
                    page = new CreateMatchPage(TeamMatch); // Create Team match
                else
                    return;

                page.Disappearing += (s, a) => Navigation.PopAsync();
                await Navigation.PushAsync(page);

            } else if (edit == "Delete")
            {

                bool answer = await Application.Current.MainPage.DisplayAlert("Delete", $"Are you sure you want to delete this?", "yes", "no");
                if (answer)
                {
                    if (PracticeSession != null)
                        RequestCreator.DeletePracticeSession(PracticeSession.Id);
                    else if (TeamMatch != null)
                        RequestCreator.DeleteTeamMatch(TeamMatch.Id);
                    else return;

                    Navigation.RemovePage(currentPage);
                    await Navigation.PopAsync();
                }
            }
            else return;
        }
    }
}
