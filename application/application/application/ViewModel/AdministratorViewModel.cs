using application.Controller;
using application.UI;
using Common.Model;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace application.ViewModel
{
    class AdministratorViewModel : BaseViewModel
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
        private ObservableCollection<FocusPointDescriptor> _focusPointList;

        public ObservableCollection<FocusPointDescriptor> FocusPointList
        {
            get { return _focusPointList; }
            set { SetProperty(ref _focusPointList, value); }
        }
        private string _searchFocusPointText;

        public string SearchFocusPointText
        {
            get { return _searchFocusPointText; }
            set { SetProperty(ref _searchFocusPointText, value);
                FocusPointList = new ObservableCollection<FocusPointDescriptor>(FocusPointList.OrderByDescending((x => StringExtension.LongestCommonSubsequence(x.Name.ToLower(), SearchFocusPointText.ToLower()))).ThenBy(x => x.Name.Length).ToList());
            }
        }
        private string _searchTeamText;

        public string SearchTeamText
        {
            get { return _searchTeamText; }
            set { SetProperty(ref _searchTeamText, value);
                TeamList = new ObservableCollection<PracticeTeam>(TeamList.OrderByDescending((x => StringExtension.LongestCommonSubsequence(x.Name.ToLower(), SearchTeamText.ToLower()))).ThenBy(x => x.Name.Length).ToList());
            }
        }
        private string _searchMemberText;

        public string SearchMemberText
        {
            get { return _searchMemberText; }
            set { SetProperty(ref _searchMemberText, value);
                MemberList = new ObservableCollection<Member>(MemberList.OrderByDescending((x => StringExtension.LongestCommonSubsequence(x.Name.ToLower(), SearchMemberText.ToLower()))).ThenBy(x => x.Name.Length).ToList());
            }
        }
        private string _newTeam;

        public string NewTeam
        {
            get { return _newTeam; }
            set { SetProperty(ref _newTeam, value); }
        }


        public AdministratorViewModel()
        {
            TeamList = new ObservableCollection<PracticeTeam>();
            TeamList.Add(new PracticeTeam() { Name = "TeamName" });
            TeamList.Add(new PracticeTeam() { Name = "TeamName2" });
            TeamList.Add(new PracticeTeam() { Name = "TeamName3" });

            MemberList = new ObservableCollection<Member>();
            MemberList.Add(new Member() { Name = "Jens" });
            MemberList.Add(new Member() { Name = "bob ross" });
            FocusPointList = new ObservableCollection<FocusPointDescriptor>();
            FocusPointList.Add(new FocusPointDescriptor() { Name = "hej" });
            FocusPointList.Add(new FocusPointDescriptor() { Name = "hej" });
            FocusPointList.Add(new FocusPointDescriptor() { Name = "hej" });
            FocusPointList.Add(new FocusPointDescriptor() { Name = "hej" });
        }

        private RelayCommand _deleteTeamCommand;

        public RelayCommand DeleteTeamCommand
        {
            get
            {
                return _deleteTeamCommand ?? (_deleteTeamCommand = new RelayCommand(param => DeleteTeamClick(param)));
            }
        }
        private void DeleteTeamClick(object param)
        {
            PracticeTeam prac = param as PracticeTeam;
            TeamList.Remove(prac);
        }



        private RelayCommand _deleteFocusPointCommand;

        public RelayCommand DeleteFocusPointCommand
        {
            get
            {
                return _deleteFocusPointCommand ?? (_deleteFocusPointCommand = new RelayCommand(param => DeleteFocusPointClick(param)));
            }
        }
        private void DeleteFocusPointClick(object param)
        {
            FocusPointDescriptor fp = param as FocusPointDescriptor;
            FocusPointList.Remove(fp);
        }
        private RelayCommand _newTeamCommand;

        public RelayCommand NewTeamCommand
        {
            get
            {
                return _newTeamCommand ?? (_newTeamCommand = new RelayCommand(param => NewTeamClick(param)));
            }
        }
        private void NewTeamClick(object param)
        {
            TeamList.Add(new PracticeTeam { Name = NewTeam });
        }
        private RelayCommand _newFocusPointCommand;

        public RelayCommand NewFocusPointCommand
        {
            get
            {
                return _newFocusPointCommand ?? (_newFocusPointCommand = new RelayCommand(param => NewFocusPointClick(param)));
            }
        }
        private void NewFocusPointClick(object param)
        {
            PopupNavigation.Instance.PushAsync(new CreateFocusPointPopupPage(false));
        }
    }
}

