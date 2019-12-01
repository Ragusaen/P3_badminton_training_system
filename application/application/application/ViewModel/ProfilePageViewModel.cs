using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using application.Controller;
using application.SystemInterface;
using application.UI;
using Common;
using Common.Model;
using Entry = Microcharts.Entry;
using Microcharts;
using SkiaSharp;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace application.ViewModel
{
    class ProfilePageViewModel : BaseViewModel
    {
        public Member Member { get; set; }
        public Player Player { get; set; }
        public Trainer Trainer { get; set; }

        private Chart _chart;

        public Chart Chart
        {
            get { return _chart; }
            set
            {
                SetProperty(ref _chart, value);
            }
        }

        private ObservableCollection<PracticeTeam> _trainerPracticeTeams;

        public ObservableCollection<PracticeTeam> TrainerPracticeTeams
        {
            get => _trainerPracticeTeams;
            set => SetProperty(ref _trainerPracticeTeams, value);
        }
        private int _trainerPracticeTeamsListHeight;

        public int TrainerPracticeTeamsListHeight
        {
            get => _trainerPracticeTeamsListHeight;
            set => SetProperty(ref _trainerPracticeTeamsListHeight, value);
        }

        private ObservableCollection<PracticeTeam> _playerPracticeTeams;

        public ObservableCollection<PracticeTeam> PlayerPracticeTeams
        {
            get => _playerPracticeTeams;
            set => SetProperty(ref _playerPracticeTeams, value);
        }

        private int _playerPracticeTeamsListHeight;

        public int PlayerPracticeTeamsListHeight
        {
            get => _playerPracticeTeamsListHeight;
            set => SetProperty(ref _playerPracticeTeamsListHeight, value);
        }

        private ObservableCollection<FocusPointItem> _focusPoints;

        public ObservableCollection<FocusPointItem> FocusPoints
        {
            get => _focusPoints;
            set => SetProperty(ref _focusPoints, value);
        }

        private int _focusPointsListHeight;

        public int FocusPointsListHeight
        {
            get => _focusPointsListHeight;
            set => SetProperty(ref _focusPointsListHeight, value); 
        }

        public string StringMemberType { get; set; } = "Neither Player nor Trainer";

        public ProfilePageViewModel(int profileId)
        {
            RequestCreator.LoggedInMember = RequestCreator.GetLoggedInMember(); // reload logged in member, because membertype might have changed
            Member = RequestCreator.GetMember(profileId);
            if (Member.MemberType != MemberType.None) // reload member, because it might have changed
            {
                if (Member.MemberType.HasFlag(MemberType.Player))
                {
                    StringMemberType = "Player";
                    Player = RequestCreator.GetPlayer(Member.Id);
                    Member = Player.Member;

                    Player.FocusPointItems = RequestCreator.GetPlayerFocusPointItems(Player.Member.Id);
                    FocusPoints = new ObservableCollection<FocusPointItem>(Player.FocusPointItems);
                    FocusPointsListHeight = FocusPoints.Count * 45;

                    Player.PracticeTeams = RequestCreator.GetPlayerPracticeTeams(Player);
                    PlayerPracticeTeams = new ObservableCollection<PracticeTeam>(Player.PracticeTeams);
                    PlayerPracticeTeamsListHeight = PlayerPracticeTeams.Count * 45;

                    List<Feedback> feedbacks = RequestCreator.GetPlayerFeedback(Member);
                    if (feedbacks.Count != 0)
                    {
                        feedbacks = feedbacks.OrderByDescending(p => p.PlaySession.Start.Date)
                            .ThenByDescending(p => p.PlaySession.Start.TimeOfDay).ToList();
                        List<Entry> entries = new List<Entry>();
                        int i = 0;
                        foreach (Feedback fb in feedbacks)
                        {
                            if (i == 15)
                                break;
                            entries.Add(
                                new Entry(((float) fb.ReadyQuestion + (float) fb.EffortQuestion +
                                           (float) fb.ChallengeQuestion + (float) fb.AbsorbQuestion) / 4)
                                {
                                    Color = SKColor.Parse("#33ccff"),
                                    ValueLabel = fb.PlaySession.Start.Date.ToString("dd/MM-yyyy")
                                });
                            i++;
                        }

                        entries.Reverse();
                        Chart = new LineChart
                        {
                            Entries = entries, LineMode = LineMode.Straight, PointMode = PointMode.Circle,
                            LabelTextSize = 25, PointSize = 12, MaxValue = 2, MinValue = -2
                        };
                    }
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

                    Trainer.PracticeTeams = RequestCreator.GetTrainerPracticeTeams(Trainer);
                    TrainerPracticeTeams = new ObservableCollection<PracticeTeam>(Trainer.PracticeTeams);
                    TrainerPracticeTeamsListHeight = TrainerPracticeTeams.Count * 45;
                    SetNoTrainerLabel();
                }
            }
            CommentText = Member?.Comment ?? "Click to add comment";
        }

        // Practice Team Section
        private bool _noTrainerVisibility;
        public bool NoTrainerVisibility
        {
            get => _noTrainerVisibility;
            set => SetProperty(ref _noTrainerVisibility, value);
        }
        private void SetNoTrainerLabel()
        {
            if (TrainerPracticeTeams == null || TrainerPracticeTeams.Count < 1)
            {
                NoTrainerVisibility = true;
            }
            else
            {
                NoTrainerVisibility = false;
            }
        }

        private RelayCommand _addPlayerPracticeTeamCommand;

        public RelayCommand AddPlayerPracticeTeamCommand
        {
            get
            {
                return _addPlayerPracticeTeamCommand ?? (_addPlayerPracticeTeamCommand = new RelayCommand(param => ExecuteAddPlayerPracticeTeam(param)));
            }
        }

        private void ExecuteAddPlayerPracticeTeam(object param)
        {
            var page = new PracticeTeamPopupPage(Player.PracticeTeams);
            page.CallBackEvent += PlayerPracticeTeamPopupPageCallback;
            PopupNavigation.Instance.PushAsync(page);
        }

        private void PlayerPracticeTeamPopupPageCallback(object sender, PracticeTeam e)
        {
            RequestCreator.SetPlayerPracticeTeams(Player, e);
            var newPracticeTeams = RequestCreator.GetPlayerPracticeTeams(Player);
            Player.PracticeTeams = newPracticeTeams;
            PlayerPracticeTeams = new ObservableCollection<PracticeTeam>(newPracticeTeams);
            PlayerPracticeTeamsListHeight = PlayerPracticeTeams.Count * 45;
        }

        // Focus Point Section
        public void PopupFocusPoint(FocusPointItem focusPoint)
        {
            var popup = new ViewFocusPointDetails(focusPoint.Descriptor);
            PopupNavigation.Instance.PushAsync(popup);
        }

        private RelayCommand _addFocusPointCommand;
        public RelayCommand AddFocusPointCommand => _addFocusPointCommand ?? (_addFocusPointCommand = new RelayCommand(ExecuteAddFocusPoint));

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
            var action = await Application.Current.MainPage.DisplayActionSheet("Settings", "Cancel", null, "Change Trainer Privileges", "Change Sex");
            if (action == "Change Trainer Privileges")
            {
                await ChangeTrainerPrivileges();
            } 
            else if (action == "Change Sex")
            {
                await ChangeSex();
            }
        }

        private async Task ChangeSex()
        {
            string newSexSelection = await Application.Current.MainPage.DisplayActionSheet("Set Sex", "Cancel", null, "Male", "Female");

            Sex newSex;

            if (newSexSelection == "Male")
            {
                newSex = Sex.Male;
            } else if (newSexSelection == "Female")
            {
                newSex = Sex.Female;
            }
            else
            {
                return;
            }

            RequestCreator.SetMemberSex(newSex, Player);
            RequestCreator.LoggedInMember = RequestCreator.GetLoggedInMember(); // reload logged in member, because changes
            Navigation.InsertPageBefore(new ProfilePage(Member.Id), Navigation.NavigationStack.Last());
            await Navigation.PopAsync();
        }

        private async Task ChangeTrainerPrivileges()
        {
            string newRights = await Application.Current.MainPage.DisplayActionSheet(_changeMemberTypeTitle,
                "Cancel", null, _changeMemberTypeQuery);

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
                return;
            }

            RequestCreator.ChangeTrainerPrivileges(Member);
            RequestCreator.LoggedInMember = RequestCreator.GetLoggedInMember(); // reload logged in member, because changes
            Navigation.InsertPageBefore(new ProfilePage(Member.Id), Navigation.NavigationStack.Last());
            await Navigation.PopAsync();
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
                PlayerPracticeTeams.Remove(practiceTeam);
                RequestCreator.DeletePlayerPracticeTeam(Player, practiceTeam);
                PlayerPracticeTeamsListHeight = PlayerPracticeTeams.Count * 45;
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
            bool answer = await Application.Current.MainPage.DisplayAlert("Delete", $"Are you sure you want to delete {focusPoint.Descriptor.Name} from this player?", "yes", "no");
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

        public bool SetComment(string comment)
        {
            if (comment.Length > 512)
            {
                Application.Current.MainPage.DisplayAlert("Invalid input", "Comment can not contain more than 512 characters", "Ok");
                return false;
            }
            RequestCreator.SetComment(Member, comment);
            return true;
        }
    }
}