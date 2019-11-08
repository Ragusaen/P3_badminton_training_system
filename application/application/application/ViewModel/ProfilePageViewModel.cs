using System;
using System.Collections.Generic;
using System.Text;
using application.Model;

namespace application.ViewModel
{
    class ProfilePageViewModel : BaseViewModel
    {
        public Member CurrentMember { get; set; } = new Member();
        
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }
        private List<PracticeTeam> _teams;

        public List<PracticeTeam> Teams
        {
            get { return _teams; }
            set
            {
                SetProperty(ref _teams, value);
            }
        }

        private string _searchtext;

        public string SearchText
        {
            get { return _searchtext; }
            set
            {
                SetProperty(ref _searchtext, value);
            }
        }

        private List<FocusPoint> _searchResultFocusPoints;

        public List<FocusPoint> SearchResultFocusPoints
        {
            get { return _searchResultFocusPoints; }
            set
            {
                SetProperty(ref _searchResultFocusPoints, value);
            }
        }

        private int _teamListHeight;

        public int TeamListHeight
        {
            get { return _teamListHeight; }
            set { SetProperty(ref _teamListHeight, value); }
        }

        private int _focusPointListHeight;

        public int FocusPointListHeight
        {
            get { return _focusPointListHeight; }
            set { SetProperty(ref _focusPointListHeight, value); }
        }


        public ProfilePageViewModel() 
        {
            Teams = new List<PracticeTeam>();
            CurrentMember.Name = "Pernille Pedersen";
            Name = CurrentMember.Name;
            Teams.Add(new PracticeTeam("U17", true));
            Teams.Add(new PracticeTeam("Senior", false));
            List<FocusPoint> focusPoint = new List<FocusPoint>();
            focusPoint.Add(new FocusPoint("Serv"));
            focusPoint.Add(new FocusPoint("Slag"));
            SearchResultFocusPoints = focusPoint;

            TeamListHeight = Teams.Count * 45;
            FocusPointListHeight = focusPoint.Count * 45;
        }
        private RelayCommand _profileSettingCommand;

        public RelayCommand ProfileSettingCommand
        {
            get
            {
                return _profileSettingCommand ?? (_profileSettingCommand = new RelayCommand(param => ExecuteProfileSettingTap(param)));
            }
        }

        //Check if user is in database. Navigate to main page.
        private void ExecuteProfileSettingTap(object param)
        {
            Name = "Hallo";
        }

        private RelayCommand _viewFeedbackCommand;

        public RelayCommand ViewFeedbackCommand
        {
            get
            {
                return _viewFeedbackCommand ?? (_viewFeedbackCommand = new RelayCommand(param => ExecuteViewFeedbackClick(param)));
            }
        }
        private void ExecuteViewFeedbackClick(object param)
        {
            
        }
    }
}
