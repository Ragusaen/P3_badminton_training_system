using application.SystemInterface;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace application.ViewModel
{
    class PracticeTeamViewModel : BaseViewModel
    {
        public PracticeTeam PracticeTeam;

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
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

        public PracticeTeamViewModel(int Id)
        {
            PracticeTeam = RequestCreator.GetPracticeTeam(Id);
            Name = PracticeTeam.Name;
            Trainer = PracticeTeam.Trainer;
            Players = new ObservableCollection<Player>(PracticeTeam.Players);
        }
    }
}
