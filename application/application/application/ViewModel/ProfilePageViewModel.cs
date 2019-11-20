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

        public ProfilePageViewModel(Member member)
        {
            Member = member;
            if (Member.MemberType != MemberType.None)
            {
                if ((Member.MemberType & MemberType.Player) > 0)
                {
                    Player = RequestCreator.GetPlayer(1);
                    Player.FocusPointItems = RequestCreator.GetPlayerFocusPointItems(Player.Member.Id);
                    FocusPoints = new ObservableCollection<FocusPointItem>(Player.FocusPointItems);
                    FocusPointListHeight = FocusPoints.Count * 45;
                }
                else if ((Member.MemberType & MemberType.Trainer) > 0)
                {
                    Trainer = new Trainer();
                }

                PracticeTeams = new ObservableCollection<PracticeTeam>(RequestCreator.GetMemberPracticeTeams(Member))
                {
                    new PracticeTeam() {Name = "U17"}, new PracticeTeam() {Name = "Senior"}
                };

                TeamListHeight = PracticeTeams.Count * 45;
            }
            CommentText = member.Comment;
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
            FocusPointPopupPage page = new FocusPointPopupPage(Player);
            page.CallBackEvent += FocusPointPopupPageCallback;
            PopupNavigation.Instance.PushAsync(page);
        }

        public void PopupFocusPoint(FocusPointItem focusPoint)
        {
            StringAndHeaderPopup popup = new StringAndHeaderPopup(focusPoint.Descriptor);
            PopupNavigation.Instance.PushAsync(popup);
        }


        private void FocusPointPopupPageCallback(object sender, FocusPointDescriptor e)
        {
            var item = new FocusPointItem
            {
                Descriptor = e,
                DateAssigned = DateTime.Now
            };
            Player.FocusPointItems.Add(item); //TODO: FIX
            FocusPoints.Add(item);

            RequestCreator.SetPlayerFocusPoints(Player, Player.FocusPointItems);

            FocusPointListHeight = FocusPoints.Count * 45;
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
            string action = await Application.Current.MainPage.DisplayActionSheet("Choose what you want to edit:", "Cancel", null, "Edit User's Password", "Edit User's Type", "Create New Focus Point");

            if (action == "Edit User's Password")
                await Navigation.PushAsync(new EditUserInfoPage(Player.Member));
            else if (action == "Edit User's Type")
            {
                string rights = await Application.Current.MainPage.DisplayActionSheet("Choose user's rights:", "Cancel", null, "Player", "Trainer", "Player and Trainer", "neither Player nor Trainer");

                if (rights == "Neither Player nor Trainer")
                    Player.Member.MemberType = MemberType.None;
                else if (rights == "Player")
                    Player.Member.MemberType = MemberType.Player;
                else if (rights == "Player")
                    Player.Member.MemberType = MemberType.Trainer;
                else if (rights == "Player and Trainer")
                    Player.Member.MemberType = MemberType.Both;
            }
            else if (action == "Create New Focus Point")
                await PopupNavigation.Instance.PushAsync(new CreateFocusPointPopupPage(false));
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
            PracticeTeam team = param as PracticeTeam;
            PracticeTeams.Remove(team);
            TeamListHeight = PracticeTeams.Count * 45; 
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
            FocusPointListHeight = FocusPoints.Count * 45;
        }


        private bool _commentVis;
        public bool CommentVis
        {
            get => _commentVis;
            set => SetProperty(ref _commentVis, value);
        }

        public bool EnterCommentVis => !CommentVis;

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