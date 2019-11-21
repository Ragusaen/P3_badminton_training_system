using application.UI;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using application.Controller;
using application.SystemInterface;
using System.Collections.ObjectModel;

namespace application.ViewModel
{
    class CreateMatchViewModel : BaseViewModel
    {
        private string _teamName;
        public string TeamName
        {
            get { return _teamName; }
            set
            {
                if (SetProperty(ref _teamName, value))
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _minDate;

        public DateTime MinDate
        {
            get { return _minDate; }
            set
            {
                SetProperty(ref _minDate, value);
            }
        }

        private DateTime _maxDate;

        public DateTime MaxDate
        {
            get { return _maxDate; }
            set
            {
                SetProperty(ref _maxDate, value);
            }
        }

        private DateTime _selectedDateStart;

        public DateTime SelectedDateStart
        {
            get { return _selectedDateStart; }
            set
            {
                if (SetProperty(ref _selectedDateStart, value))
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _selectedDateEnd;

        public DateTime SelectedDateEnd
        {
            get { return _selectedDateEnd; }
            set
            {
                if (SetProperty(ref _selectedDateEnd, value))
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _selectedTimeStart;

        public string SelectedTimeStart
        {
            get { return _selectedTimeStart; }
            set
            {
                if (SetProperty(ref _selectedTimeStart, value))
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _selectedTimeEnd;

        public string SelectedTimeEnd
        {
            get { return _selectedTimeEnd; }
            set
            {
                if (SetProperty(ref _selectedTimeEnd, value))
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _matchResponsibleName;

        public string MatchResponsibleName
        {
            get { return _matchResponsibleName; }
            set
            {
                if (SetProperty(ref _matchResponsibleName, value))
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                if (SetProperty(ref _location, value))
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
            }
        }

        public List<string> LeagueNames
        {
            get { return Enum.GetNames(typeof(TeamMatch.Leagues)).Select(p => StringExtension.SplitCamelCase(p)).ToList(); }
        }

        private TeamMatch.Leagues _selectedLeague;
        public TeamMatch.Leagues SelectedLeague
        {
            get { return _selectedLeague; }
            set { SetProperty(ref _selectedLeague, value); }
        }


        private Dictionary<Tuple<Lineup.PositionType, int>, Position> _positions;

        public Dictionary<Tuple<Lineup.PositionType, int>, Position> Positions
        {
            get { return _positions; }
            set { SetProperty(ref _positions, value); }
        }

        private ObservableCollection<Player> _players;

        public ObservableCollection<Player> Players
        {
            get { return _players; }
            set { SetProperty(ref _players, value); }
        }

        public CreateMatchViewModel()
        {
            Players = new ObservableCollection<Player>(RequestCreator.GetAllPlayers());

            Positions = new Dictionary<Tuple<Lineup.PositionType, int>, Position>();

            Position p = new Position();
            p.Player = new Player() { BadmintonPlayerId = 1234, Member = new Member() { Name = "Bob" } };
            p.OtherPlayer = new Player() { BadmintonPlayerId = 4321, Member = new Member() { Name = "Jens" } };

            Position q = new Position();
            q.Player = new Player() { BadmintonPlayerId = 1234, Member = new Member() { Name = "Jens" } };

            Positions.Add(new Tuple<Lineup.PositionType, int>(Lineup.PositionType.MensDouble, 1), p);
            Positions.Add(new Tuple<Lineup.PositionType, int>(Lineup.PositionType.MensSingle, 2), q);
        }

        private RelayCommand _saveMatchClickCommand;

        public RelayCommand SaveMatchClickCommand
        {
            get
            {
                return _saveMatchClickCommand ?? (_saveMatchClickCommand = new RelayCommand(param => ExecuteSaveMatchClick(param), param => CanExecuteSaveMatchClick(param)));
            }
        }

        private bool CanExecuteSaveMatchClick(object param)
        {
            if (string.IsNullOrEmpty(TeamName) || (SelectedDateStart == null) || (SelectedDateEnd == null) ||
                (SelectedTimeStart == null) || (SelectedTimeEnd == null))
                return false;
            else
                return true;
        }

        private void ExecuteSaveMatchClick(object param)
        {
            //TODO: Update model
            //Navigate back
            Navigation.PopAsync();
        }
    }
}
