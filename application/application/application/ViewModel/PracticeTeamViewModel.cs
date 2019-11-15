using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace application.ViewModel
{
    class PracticeTeamViewModel : BaseViewModel
    {
        private ObservableCollection<PracticeTeam> _teamList;

        public ObservableCollection<PracticeTeam> TeamList
        {
            get { return _teamList; }
            set { SetProperty(ref _teamList, value); }
        }

        private ObservableCollection<Member> _memberList;

        public ObservableCollection<Member> MemberList
        {
            get { return _memberList; }
            set { SetProperty(ref _memberList, value); }
        }

        public PracticeTeamViewModel()
        {
            TeamList = new ObservableCollection<PracticeTeam>();
            TeamList.Add(new PracticeTeam() { Name = "TeamName" });
            TeamList.Add(new PracticeTeam() { Name = "TeamName2" });
            TeamList.Add(new PracticeTeam() { Name = "TeamName3" });

            MemberList = new ObservableCollection<Member>();
            MemberList.Add(new Member() { Name = "Jens" });
            MemberList.Add(new Member() { Name = "bob ross" });
        }
    }
}
