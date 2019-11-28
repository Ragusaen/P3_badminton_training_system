using application.Controller;
using application.UI;
using Common.Model;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using application.SystemInterface;
using Xamarin.Forms;

namespace application.ViewModel
{
    class AdministratorViewModel : BaseViewModel
    {
        private ObservableCollection<PracticeTeam> _practiceTeamList;
        public ObservableCollection<PracticeTeam> PracticeTeamList
        {
            get => _practiceTeamList;
            set => SetProperty(ref _practiceTeamList, value);
        }

        private ObservableCollection<Member> _memberList;
        public ObservableCollection<Member> MemberList
        {
            get => _memberList;
            set => SetProperty(ref _memberList, value);
        }

        private ObservableCollection<FocusPointDescriptor> _focusPointList;
        public ObservableCollection<FocusPointDescriptor> FocusPointList
        {
            get => _focusPointList;
            set => SetProperty(ref _focusPointList, value); 
        }

        private string _searchFocusPointText;
        public string SearchFocusPointText
        {
            get => _searchFocusPointText;
            set { SetProperty(ref _searchFocusPointText, value);
                FocusPointList = new ObservableCollection<FocusPointDescriptor>(FocusPointList.OrderByDescending((x => StringExtension.LongestCommonSubsequence(x.Name.ToLower(), SearchFocusPointText.ToLower()))).ThenBy(x => x.Name.Length).ToList());
            }
        }

        private string _searchTeamText;
        public string SearchTeamText
        {
            get { return _searchTeamText; }
            set { SetProperty(ref _searchTeamText, value);
                PracticeTeamList = new ObservableCollection<PracticeTeam>(PracticeTeamList.OrderByDescending((x => StringExtension.LongestCommonSubsequence(x.Name.ToLower(), SearchTeamText.ToLower()))).ThenBy(x => x.Name.Length).ToList());
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

        private string _newPracticeTeam;
        public string NewPracticeTeam
        {
            get { return _newPracticeTeam; }
            set
            {
                if(SetProperty(ref _newPracticeTeam, value))
                    NewPracticeTeamCommand.RaiseCanExecuteChanged();
            }
        }


        public AdministratorViewModel()
        {
            var pageInfo = RequestCreator.GetAdminPage();
            PracticeTeamList = new ObservableCollection<PracticeTeam>(pageInfo.practiceTeams);
            MemberList = new ObservableCollection<Member>(pageInfo.members);
            FocusPointList = new ObservableCollection<FocusPointDescriptor>(pageInfo.focusPoints);
        }


        private RelayCommand _deletePracticeTeamCommand;
        public RelayCommand DeletePracticeTeamCommand => _deletePracticeTeamCommand ?? (_deletePracticeTeamCommand = new RelayCommand(DeletePracticeTeamClick));


        private async void DeletePracticeTeamClick(object param)
        {
            PracticeTeam prac = param as PracticeTeam;
            bool answer = await Application.Current.MainPage.DisplayAlert("Delete", $"Are you sure you want to delete {prac.Name}?", "yes", "no");
            if (answer)
            {
                PracticeTeamList.Remove(prac);
                RequestCreator.DeletePracticeTeam(prac);
            }
        }

        private RelayCommand _editFocusPointCommand;
        public RelayCommand EditFocusPointCommand => _editFocusPointCommand ?? (_editFocusPointCommand = new RelayCommand(EditFocusPointClick));

        private async void EditFocusPointClick(object param)
        {
            var fp = param as FocusPointDescriptor;
            await PopupNavigation.Instance.PushAsync(new CreateFocusPointPopupPage(false, fp));
            FocusPointList = new ObservableCollection<FocusPointDescriptor>(RequestCreator.GetFocusPoints());
        }

        private RelayCommand _deleteFocusPointCommand;
        public RelayCommand DeleteFocusPointCommand => _deleteFocusPointCommand ?? (_deleteFocusPointCommand = new RelayCommand(param => DeleteFocusPointClick(param)));

        private async void DeleteFocusPointClick(object param)
        {
            FocusPointDescriptor fp = param as FocusPointDescriptor;
            bool answer = await Application.Current.MainPage.DisplayAlert("Delete", $"Are you sure you want to delete {fp.Name}?", "yes", "no");
            if (answer)
            {
                FocusPointList.Remove(fp);
                RequestCreator.DeleteFocusPointDescriptor(fp);
                FocusPointList = new ObservableCollection<FocusPointDescriptor>(RequestCreator.GetFocusPoints());
            }
        }

        private RelayCommand _newPracticeTeamCommand;
        public RelayCommand NewPracticeTeamCommand => _newPracticeTeamCommand ?? (_newPracticeTeamCommand = new RelayCommand(NewPracticeTeamClick, CanCreateNewPracticeTeam));

        private void NewPracticeTeamClick(object param)
        {
            var team = new PracticeTeam {Name = NewPracticeTeam};
            RequestCreator.SetPracticeTeam(team);
            PracticeTeamList = new ObservableCollection<PracticeTeam>(RequestCreator.GetAllPracticeTeams());
            NewPracticeTeam = null;
        }

        private bool CanCreateNewPracticeTeam(object param)
        {
            return !string.IsNullOrEmpty(NewPracticeTeam)
                   && !string.IsNullOrWhiteSpace(NewPracticeTeam);
        }


        private RelayCommand _newFocusPointCommand;
        public RelayCommand NewFocusPointCommand =>_newFocusPointCommand ?? (_newFocusPointCommand = new RelayCommand(NewFocusPointClick));

        private void NewFocusPointClick(object param)
        {
            var newPage = new CreateFocusPointPopupPage(false);
            PopupNavigation.Instance.PushAsync(newPage);
            ((CreateFocusPointPopupViewModel)newPage.BindingContext).CallBackEvent += OnCallBackEvent;
        }

        private void OnCallBackEvent(object sender, FocusPointDescriptor e)
        {
            FocusPointList = new ObservableCollection<FocusPointDescriptor>(RequestCreator.GetFocusPoints());
        }

        public void PopupFocusPoint(FocusPointDescriptor focusPoint)
        {
            var popup = new ViewFocusPointDetails(focusPoint);
            PopupNavigation.Instance.PushAsync(popup);
        }
    }
}

