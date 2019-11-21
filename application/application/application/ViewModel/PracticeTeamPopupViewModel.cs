using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using application.Controller;
using application.SystemInterface;
using Common.Model;

namespace application.ViewModel
{
    class PracticeTeamPopupViewModel : BaseViewModel
    {
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                /*if (string.IsNullOrEmpty(_searchtext))
                    FocusPoints.OrderByDescending(p => p.Descriptor.Name);
                else*/
                FilteredPracticeTeams = new ObservableCollection<PracticeTeam>(_filteredPracticeTeams.OrderByDescending(
                        x => StringSearch.LongestCommonSubsequence(x.Name.ToLower(), SearchText.ToLower()))
                        .ThenBy(x => x.Name.Length).ToList());
            }
        }

        private ObservableCollection<PracticeTeam> _filteredPracticeTeams;
        public ObservableCollection<PracticeTeam> FilteredPracticeTeams
        {
            get => _filteredPracticeTeams;
            set => SetProperty(ref _filteredPracticeTeams, value);
        }

        public PracticeTeamPopupViewModel(List<PracticeTeam> practiceTeams)
        {
            var notShown = practiceTeams;
            var allPracticeTeams = RequestCreator.GetAllPracticeTeams();
            var filteredPracticeTeams = allPracticeTeams.Where(p => notShown.All(q => q.Id != p.Id)).ToList();
            FilteredPracticeTeams = new ObservableCollection<PracticeTeam>(filteredPracticeTeams);
        }
    }
}
