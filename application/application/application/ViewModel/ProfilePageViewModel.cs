using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using application.Controller;
using application.SystemInterface;
using application.UI;
using Common;
using Common.Model;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace application.ViewModel
{
    class ProfilePageViewModel : BaseViewModel
    {
        public Member Member { get; set; }
        public Player Player { get; set; }
        public Trainer Trainer { get; set; }

        private ObservableCollection<PracticeTeam> _practiceTeams;

        public ObservableCollection<PracticeTeam> PracticeTeams
        {
            get { return _practiceTeams; }
            set
            {
                SetProperty(ref _practiceTeams, value);
            }
        }

        private ObservableCollection<FocusPointItem> _focusPoints;

        public ObservableCollection<FocusPointItem> FocusPoints
        {
            get { return _focusPoints; }
            set { SetProperty(ref _focusPoints, value); }
        }

        private int _practiceTeamsListHeight;

        public int PracticeTeamsListHeight
        {
            get { return _practiceTeamsListHeight; }
            set { SetProperty(ref _practiceTeamsListHeight, value); }
        }

        private int _focusPointsListHeight;

        public int FocusPointsListHeight
        {
            get => _focusPointsListHeight;
            set => SetProperty(ref _focusPointsListHeight, value); 
        }

        public string StringMemberType { get; set; }

        public ProfilePageViewModel(Member member)
        {
            StringMemberType = "Neither Player nor Trainer";
            RequestCreator.LoggedInMember = RequestCreator.GetLoggedInMember(); // reload logged in member, because membertype might have changed
            Member = member;
            if (Member.MemberType != MemberType.None)
            {
                if (Member.MemberType.HasFlag(MemberType.Player))
                {
                    StringMemberType = "Player";
                    Player = RequestCreator.GetPlayer(Member.Id);
                    Player.Member = Member;

                    Player.FocusPointItems = RequestCreator.GetPlayerFocusPointItems(Player.Member.Id);
                    FocusPoints = new ObservableCollection<FocusPointItem>(Player.FocusPointItems);
                    FocusPointsListHeight = FocusPoints.Count * 45;

                    Player.PracticeTeams = RequestCreator.GetPlayerPracticeTeams(Player);
                    PracticeTeams = new ObservableCollection<PracticeTeam>(Player.PracticeTeams);
                    PracticeTeamsListHeight = PracticeTeams.Count * 45;
                }
                if (Member.MemberType.HasFlag(MemberType.Trainer))
                {
                    Trainer = new Trainer {Member = Member};
                    _changeMemberTypeTitle = "This Member Is a Trainer";
                    _changeMemberTypeQuery = "Unmake Trainer";

                    if (Member.MemberType == MemberType.Both)
                        StringMemberType = "Both Player and Trainer";
                    else
                        StringMemberType = "Trainer";

                    //TODO: handler for fetching a trainers practice teams
                    //Trainer.PracticeTeams = RequestCreator.GetTrainerPracticeTeams(Trainer);
                    //PracticeTeams = new ObservableCollection<PracticeTeam>(Trainer.PracticeTeams);
                }
            }
            CommentText = member?.Comment ?? "Click to add comment";
        }

        private void Load()
        {

        }

        // Practice Team Section
        private RelayCommand _addPracticeTeamCommand;

        public RelayCommand AddPracticeTeamCommand
        {
            get
            {
                return _addPracticeTeamCommand ?? (_addPracticeTeamCommand = new RelayCommand(param => ExecuteAddPracticeTeam(param)));
            }
        }

        private void ExecuteAddPracticeTeam(object param)
        {
            PracticeTeamPopupPage page = new PracticeTeamPopupPage(Player.PracticeTeams);
            page.CallBackEvent += PracticeTeamPopupPageCallback;
            PopupNavigation.Instance.PushAsync(page);
        }

        private void PracticeTeamPopupPageCallback(object sender, PracticeTeam e)
        {
            Player.PracticeTeams.Add(e);
            PracticeTeams.Add(e);
            PracticeTeams = new ObservableCollection<PracticeTeam>(PracticeTeams);
            RequestCreator.SetPlayerPracticeTeams(Player, Player.PracticeTeams);
            PracticeTeamsListHeight = PracticeTeams.Count * 45;
        }

        // Focus Point Section
        public void PopupFocusPoint(FocusPointItem focusPoint)
        {
            StringAndHeaderPopup popup = new StringAndHeaderPopup(focusPoint.Descriptor);
            PopupNavigation.Instance.PushAsync(popup);
        }

        private RelayCommand _addFocusPointCommand;

        public RelayCommand AddFocusPointCommand
        {
            get 
            { 
                return _addFocusPointCommand ?? (_addFocusPointCommand = new RelayCommand(param => ExecuteAddFocusPoint(param))); 
            }
        }

        private void ExecuteAddFocusPoint(object param)
        {
            FocusPointPopupPage page = new FocusPointPopupPage(Player.FocusPointItems, Player);
            ((FocusPointPopupViewModel)page.BindingContext).CallBackEvent += FocusPointPopupPageCallback;
            PopupNavigation.Instance.PushAsync(page);
        }

        private void FocusPointPopupPageCallback(object sender, FocusPointDescriptor e)
        {
            RequestCreator.SetPlayerFocusPoints(Player, e);
            Player.FocusPointItems = RequestCreator.GetPlayerFocusPointItems(Player.Member.Id);
            FocusPoints = new ObservableCollection<FocusPointItem>(Player.FocusPointItems);
            FocusPointsListHeight = FocusPoints.Count * 45;
        }

        private RelayCommand _profileSettingCommand;

        public RelayCommand ProfileSettingCommand
        {
            get
            {
                return _profileSettingCommand ?? (_profileSettingCommand = new RelayCommand(param => ExecuteProfileSettingTap(param)));
            }
        }

        private readonly string _changeMemberTypeTitle = "This Member Is Not a Trainer";
        private readonly string _changeMemberTypeQuery = "Make Trainer";
        private async void ExecuteProfileSettingTap(object param)
        {
            await Application.Current.MainPage.DisplayActionSheet("Settings", "Cancel", null, "Change Member Type");
            string newRights = await Application.Current.MainPage.DisplayActionSheet(_changeMemberTypeTitle, "Cancel", null, _changeMemberTypeQuery);

            if (newRights == "Make Trainer")
            {
                Member.MemberType |= MemberType.Trainer;

            }
            else if (newRights == "Unmake Trainer")
            {
                Member.MemberType &= ~MemberType.Trainer;
            }
            else
            {
                goto here;
            }
            RequestCreator.ChangeTrainerPrivileges(Member);
            RequestCreator.LoggedInMember = RequestCreator.GetLoggedInMember(); // reload logged in member, because membertype might have changed
            Navigation.InsertPageBefore(new ProfilePage(Member), Navigation.NavigationStack.Last());
            await Navigation.PopAsync();
            here:;

            /*if(RequestCreator.LoggedInMember.MemberType.HasFlag(MemberType.Trainer)) // No support for change password, thus commented away
            {
                action = await Application.Current.MainPage.DisplayActionSheet("Settings", "Cancel", null, "Change Member Type");
            }
            else
            {
                action = await Application.Current.MainPage.DisplayActionSheet("Settings", "Cancel", null, "Change Password");
            }

            if (action == "Change Password")
                await Navigation.PushAsync(new EditUserInfoPage(Member));
            else if (action == "Change Member Type")
            {
                string newRights = await Application.Current.MainPage.DisplayActionSheet(_changeMemberTypeTitle, "Cancel", null, _changeMemberTypeQuery);

                if (newRights == "Make Trainer")
                {
                    Member.MemberType |= MemberType.Trainer;

                }
                else if (newRights == "Unmake Trainer")
                {
                    Member.MemberType &= ~MemberType.Trainer;
                }
                else
                {
                    goto here;
                }
                RequestCreator.ChangeTrainerPrivileges(Member);
                Navigation.InsertPageBefore(new ProfilePage(Member), Navigation.NavigationStack.Last());
                Navigation.PopAsync();
                here: ;
            }*/
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
            Navigation.PushAsync(new ViewDetailedFeedbackPage(Player));
        }

        private RelayCommand _viewFeedbackGraphCommand;

        public RelayCommand ViewFeedbackGraphCommand
        {
            get
            {
                return _viewFeedbackGraphCommand ?? (_viewFeedbackGraphCommand = new RelayCommand(param => ExecuteViewFeedbackGraphClick(param)));
            }
        }
        private void ExecuteViewFeedbackGraphClick(object param)
        {
            Navigation.PushAsync(new ViewFeedbackPage(Player));
        }
        private RelayCommand _deleteListPlayerPracticeTeamCommand;

        public RelayCommand DeleteListPlayerPracticeTeamCommand
        {
            get
            {
                return _deleteListPlayerPracticeTeamCommand ?? (_deleteListPlayerPracticeTeamCommand = new RelayCommand(param => DeleteListPlayerPracticeTeamClick(param)));
            }
        }
        private async void DeleteListPlayerPracticeTeamClick(object param)
        {
            PracticeTeam practiceTeam = param as PracticeTeam;
            bool answer = await Application.Current.MainPage.DisplayAlert("Delete", $"Are you sure you want to delete {practiceTeam.Name}?", "yes", "no");
            if(answer) {Player.PracticeTeams.Remove(practiceTeam);
                PracticeTeams.Remove(practiceTeam);
                RequestCreator.DeletePlayerPracticeTeam(Player, practiceTeam);
                PracticeTeamsListHeight = PracticeTeams.Count * 45;
            }
        }
        private RelayCommand _deleteListFocusItemCommand;

        public RelayCommand DeleteListFocusItemCommand
        {
            get
            {
                return _deleteListFocusItemCommand ?? (_deleteListFocusItemCommand = new RelayCommand(param => DeleteListFocusItemClick(param)));
            }
        }
        private async void DeleteListFocusItemClick(object param)
        {
            FocusPointItem focusPoint = param as FocusPointItem;
            bool answer = await Application.Current.MainPage.DisplayAlert("Delete", $"Are you sure you want to delete {focusPoint.Descriptor.Name}?", "yes", "no");
            if (answer)
            {
                FocusPoints.Remove(focusPoint);
                Player.FocusPointItems.Remove(focusPoint);
                RequestCreator.DeletePlayerFocusPoints(Player, focusPoint);
                FocusPointsListHeight = FocusPoints.Count * 45;
            }
        }

        private string _commentText;    

        public string CommentText
        {
            get => _commentText;
            set => SetProperty(ref _commentText, value);
        }

        public void SetComment(string comment)
        {
            Debug.WriteLine(Member.Name);
            RequestCreator.SetComment(Member, comment);
        }
    }
}