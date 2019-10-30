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
        public ProfilePageViewModel() 
        {
            Teams = new List<PracticeTeam>();
            CurrentMember.Name = "Pernille Pedersen";
            Name = CurrentMember.Name;
            Teams.Add(new PracticeTeam("U17", true));
            Teams.Add(new PracticeTeam("Senior", false));
            List<FocusPoint> FocusPoint = new List<FocusPoint>();
            FocusPoint.Add(new FocusPoint("Serv"));
            FocusPoint.Add(new FocusPoint("Slag"));
            SearchResultFocusPoints = FocusPoint;
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
    }
}
