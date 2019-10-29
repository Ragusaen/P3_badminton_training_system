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


        public ProfilePageViewModel() 
        {
            Teams = new List<PracticeTeam>();
            CurrentMember.Name = "Pernille Pedersen";
            Name = CurrentMember.Name;
            Teams.Add(new PracticeTeam("U17", true));
            Teams.Add(new PracticeTeam("Senior", false));

        }
        private RelayCommand _teamToggleCommand;

        public RelayCommand TeamToggleCommand
        {
            get
            {
                return _teamToggleCommand ?? (_teamToggleCommand = new RelayCommand(param => ExecuteTeamToogle(param)));
            }
        }

        //Check if user is in database. Navigate to main page.
        private void ExecuteTeamToogle(object param)
        {

        }
    }
}
