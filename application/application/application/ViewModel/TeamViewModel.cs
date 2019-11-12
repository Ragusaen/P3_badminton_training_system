using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace application.ViewModel
{
    class TeamViewModel : BaseViewModel
    {

        private string _searchtext;

        public string SearchText
        {
            get { return _searchtext; }
            set
            {
                SetProperty(ref _searchtext, value);
            }
        }
        private ObservableCollection<Team> _teamList;

        public ObservableCollection<Team> TeamList
        {
            get { return _teamList; }
            set { SetProperty(ref _teamList, value); }
        }
        
        public TeamViewModel()
        {
            TeamList = new ObservableCollection<Team>();
            TeamList.Add(new Team() { Name = "TeamName"});
        }
    }
}
