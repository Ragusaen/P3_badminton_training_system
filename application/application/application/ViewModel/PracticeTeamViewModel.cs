using application.SystemInterface;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using application.UI;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace application.ViewModel
{
    class PracticeTeamViewModel : BaseViewModel
    {
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
            PracticeTeam = RequestCreator.GetPracticeTeam(id);
            Trainer = PracticeTeam.Trainer;
            Players = new ObservableCollection<Player>(PracticeTeam.Players);
            PlayerListHeight = Players.Count * 45;
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

                PracticeTeam = RequestCreator.GetPracticeTeam(PracticeTeam.Id);
                Trainer = PracticeTeam.Trainer;
                Players = new ObservableCollection<Player>(PracticeTeam.Players);
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
    }
}
