using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using application.Controller;
using application.SystemInterface;
using application.UI;
using Common.Model;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace application.ViewModel
{
    class ProfilePageViewModel : BaseViewModel
    {
        public Member Member { get; set; }
        public Player Player { get; set; }
        public bool IsPlayer { get; set; }
        public Trainer Trainer { get; set; }
        public bool IsTrainer { get; set; }
        public ProfilePageViewModel() { }

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
            get { return _focusPointsListHeight; }
            set { SetProperty(ref _focusPointsListHeight, value); }
        }

        public ProfilePageViewModel(Member member)
        {
            Member = member;
            if (Member.MemberType != MemberType.None)
            {
                if ((Member.MemberType & MemberType.Player) > 0)
                {
                    Player = RequestCreator.GetPlayer(Member.Id);

                    Player.FocusPointItems = RequestCreator.GetPlayerFocusPointItems(Player.Member.Id);
                    FocusPoints = new ObservableCollection<FocusPointItem>(Player.FocusPointItems);
                    FocusPointsListHeight = FocusPoints.Count * 45;

                    Player.PracticeTeams = RequestCreator.GetPlayerPracticeTeams(Player);
                    PracticeTeams = new ObservableCollection<PracticeTeam>(Player.PracticeTeams);
                    PracticeTeamsListHeight = PracticeTeams.Count * 45;
                }
                else if ((Member.MemberType & MemberType.Trainer) > 0)
                {
                    Trainer = new Trainer();
                }
            }
            CommentText = member?.Comment ?? "Click to add comment";
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
            FocusPointPopupPage page = new FocusPointPopupPage(Player.FocusPointItems);
            page.CallBackEvent += FocusPointPopupPageCallback;
            PopupNavigation.Instance.PushAsync(page);
        }

        private void FocusPointPopupPageCallback(object sender, FocusPointDescriptor e)
        {
            var item = new FocusPointItem
            {
                Descriptor = e
            };
            Player.FocusPointItems.Add(item);
            FocusPoints.Add(item);

            RequestCreator.SetPlayerFocusPoints(Player, Player.FocusPointItems);

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

        private async void ExecuteProfileSettingTap(object param)
        {
            string action = await Application.Current.MainPage.DisplayActionSheet("Settings", "Cancel", null, "Change Password", "Change Member Type");

            if (action == "Change Password")
                await Navigation.PushAsync(new EditUserInfoPage(Member));
            else if (action == "Change Member Type")
            {
                string rights = await Application.Current.MainPage.DisplayActionSheet("Choose a Member Type", "Cancel", null, "Player", "Trainer", "Player and Trainer", "Neither Player nor Trainer");

                if (rights == "Neither Player nor Trainer")
                    Player.Member.MemberType = MemberType.None;
                else if (rights == "Player")
                    Player.Member.MemberType = MemberType.Player;
                else if (rights == "Trainer")
                    Player.Member.MemberType = MemberType.Trainer;
                else if (rights == "Player and Trainer")
                    Player.Member.MemberType = MemberType.Both;
            }
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
        private RelayCommand _deleteListTeamItemCommand;

        public RelayCommand DeleteListTeamItemCommand
        {
            get
            {
                return _deleteListTeamItemCommand ?? (_deleteListTeamItemCommand = new RelayCommand(param => DeleteListTeamItemClick(param)));
            }
        }
        private void DeleteListTeamItemClick(object param)
        {
            PracticeTeam practiceTeam = param as PracticeTeam;
            PracticeTeams.Remove(practiceTeam);
            PracticeTeamsListHeight = PracticeTeams.Count * 45;
            //RequestCreator.DeletePlayerPracticeTeam(Member.Id, practiceTeam);
        }
        private RelayCommand _deleteListFocusItemCommand;

        public RelayCommand DeleteListFocusItemCommand
        {
            get
            {
                return _deleteListFocusItemCommand ?? (_deleteListFocusItemCommand = new RelayCommand(param => DeleteListFocusItemClick(param)));
            }
        }
        private void DeleteListFocusItemClick(object param)
        {
            FocusPointItem focusPoint = param as FocusPointItem;
            FocusPoints.Remove(focusPoint);
            Player.FocusPointItems.Remove(focusPoint);
            RequestCreator.DeletePlayerFocusPoints(Player.Member.Id, focusPoint);
            FocusPointsListHeight = FocusPoints.Count * 45;
        }

        private string _commentText;    

        public string CommentText
        {
            get => _commentText;
            set => SetProperty(ref _commentText, value);
        }

        public void SetComment(string comment)
        {
            RequestCreator.SetComment(Member, comment);
        }
    }
}