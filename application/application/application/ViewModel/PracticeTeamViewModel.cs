using application.SystemInterface;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using application.Controller;
using application.UI;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace application.ViewModel
{
    class PracticeTeamViewModel : BaseViewModel
    {
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                if (string.IsNullOrEmpty(_searchText))
                    Players = new ObservableCollection<Player>(Players.OrderBy(p => p.Member.Name).ToList());
                else
                {
                    Players = new ObservableCollection<Player>(_players.OrderByDescending(
                            x => StringExtension.LongestCommonSubsequence(x.Member.Name.ToLower(), SearchText.ToLower()))
                        .ThenBy(x => x.Member.Name.Length).ToList());
                }
            }
        }

        private PracticeTeam _practiceTeam;

        public PracticeTeam PracticeTeam
        {
            get => _practiceTeam;
            set => SetProperty(ref _practiceTeam, value);
        }

        private Trainer _trainer;
        public Trainer Trainer
        {
            get => _trainer;
            set => SetProperty(ref _trainer, value);
        }

        private ObservableCollection<Player> _players;
        public ObservableCollection<Player> Players
        {
            get => _players; 
            set => SetProperty(ref _players, value);
        }

        private int _playerListHeight;
        public int PlayerListHeight
        {
            get => _playerListHeight;
            set => SetProperty(ref _playerListHeight, value);
        }

        public PracticeTeamViewModel(int id)
        {
            var teamResponse = RequestCreator.GetPracticeTeam(id);
            PracticeTeam = teamResponse.team;
            Trainer = PracticeTeam.Trainer;
            Players = new ObservableCollection<Player>(teamResponse.players);
            PlayerListHeight = Players.Count * 45;

            SearchText = null;
        }

        private RelayCommand _removePlayerCommand;
        public RelayCommand RemovePlayerCommand => _removePlayerCommand ?? (_removePlayerCommand = new RelayCommand(RemovePlayerClick));

        private async void RemovePlayerClick(object param)
        {
            var player = param as Player;
            bool answer = await Application.Current.MainPage.DisplayAlert("Remove", $"Remove {player.Member.Name} from this practice team?", "yes", "no");
            if (answer)
            {
                RequestCreator.DeletePlayerPracticeTeam(player, PracticeTeam);

                var teamResponse = RequestCreator.GetPracticeTeam(PracticeTeam.Id);
                PracticeTeam = teamResponse.team;
                Trainer = PracticeTeam.Trainer;
                Players = new ObservableCollection<Player>(teamResponse.players);
                PlayerListHeight = Players.Count * 45;
            }
        }

        private RelayCommand _trainerViewCommand;
        public RelayCommand TrainerViewCommand => _trainerViewCommand ?? (_trainerViewCommand = new RelayCommand(param => TrainerClick()));

        private async void TrainerClick()
        {
            await Navigation.PushAsync(new ProfilePage(Trainer.Member.Id));
        }

        private RelayCommand _newTrainerCommand;
        public RelayCommand NewTrainerCommand => _newTrainerCommand ?? (_newTrainerCommand = new RelayCommand(NewTrainerClick));

        private async void NewTrainerClick(object param)
        {
            var popup = new ChooseTrainerPopupPage();
            popup.CallBackEvent += ChooseTrainerPopupPageCallback;
            await PopupNavigation.Instance.PushAsync(popup);
        }

        private async void ChooseTrainerPopupPageCallback(object sender, Trainer e)
        {
            RequestCreator.SetPracticeTeamTrainer(PracticeTeam, e);
            Navigation.InsertPageBefore(new PracticeTeamPage(PracticeTeam.Id), Navigation.NavigationStack.Last());
            await Navigation.PopAsync();
        }

        private RelayCommand _addPlayerCommand;
        public RelayCommand AddPlayerCommand => _addPlayerCommand ?? (_addPlayerCommand = new RelayCommand(AddPlayerClick));

        private async void AddPlayerClick(object param)
        {
            var popup = new ChoosePlayerPopupPage(Players.ToList());
            popup.CallBackEvent += ChoosePlayerPopupPageCallback;
            await PopupNavigation.Instance.PushAsync(popup);
        }

        private async void ChoosePlayerPopupPageCallback(object sender, Player e)
        {
            RequestCreator.SetPlayerPracticeTeams(e, PracticeTeam);
            Navigation.InsertPageBefore(new PracticeTeamPage(PracticeTeam.Id), Navigation.NavigationStack.Last());
            await Navigation.PopAsync();
        }
    }
}
