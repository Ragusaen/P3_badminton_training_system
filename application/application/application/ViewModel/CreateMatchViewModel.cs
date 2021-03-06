﻿using application.UI;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using application.Controller;
using application.SystemInterface;
using System.Collections.ObjectModel;
using Common;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace application.ViewModel
{
    class CreateMatchViewModel : BaseViewModel
    {
        private bool _reservesVisible;

        public bool ReservesVisible
        {
            get { return _reservesVisible; }
            set { SetProperty(ref _reservesVisible, value); }
        }

        private Color _verifyButtonColor;

        public Color VerifyButtonColor
        {
            get { return _verifyButtonColor; }
            set { SetProperty(ref _verifyButtonColor, value); }
        }

        private string _opponentName;

        public string OpponentName
        {
            get { return _opponentName; }
            set
            {
                if(SetProperty(ref _opponentName, value))
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTime MinDate { get; set; } = DateTime.Today;

        private DateTime _selectedDateStart;

        public DateTime SelectedDateStart
        {
            get { return _selectedDateStart; }
            set
            {
                if (SetProperty(ref _selectedDateStart, value))
                {
                    Season = value.Year;
                    if (value.Month <= 6)
                        Season--;
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private TimeSpan _selectedTimeStart;

        public TimeSpan SelectedTimeStart
        {
            get { return _selectedTimeStart; }
            set
            {
                if (SetProperty(ref _selectedTimeStart, value))
                {
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
                    if (SelectedTimeStart > _selectedTimeEnd)
                        SelectedTimeEnd = SelectedTimeStart;
                }
            }
        }

        private TimeSpan _selectedTimeEnd;

        public TimeSpan SelectedTimeEnd
        {
            get { return _selectedTimeEnd; }
            set
            {
                if (SetProperty(ref _selectedTimeEnd, value))
                {
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
                    if (SelectedTimeStart > _selectedTimeEnd)
                        SelectedTimeStart = SelectedTimeEnd;
                }
            }
        }

        private Member _captain;

        public Member Captain
        {
            get { return _captain; }
            set
            {
                if (SetProperty(ref _captain, value))
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

        private int? _season;

        public int? Season
        {
            get { return _season; }
            set
            {
                if (SetProperty(ref _season, value))
                {
                    VerifyLineupCommand.RaiseCanExecuteChanged();
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private int? _leagueRound;

        public int? LeagueRound
        {
            get { return _leagueRound; }
            set 
            {
                if (SetProperty(ref _leagueRound, value))
                {
                    VerifyLineupCommand.RaiseCanExecuteChanged();
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private int? _teamIndex;

        public int? TeamIndex
        {
            get { return _teamIndex; }
            set
            {
                if (SetProperty(ref _teamIndex, value))
                {
                    VerifyLineupCommand.RaiseCanExecuteChanged();
                    SaveMatchClickCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private int _lineupHeight;
        public int LineupHeight
        {
            get => _lineupHeight;
            set => SetProperty(ref _lineupHeight, value);
        }

        public List<string> LeagueNames
        {
            get { return Enum.GetNames(typeof(TeamMatch.Leagues)).Select(p => StringExtension.SplitCamelCase(p)).ToList(); }
        }
        
        private TeamMatch.Leagues _selectedLeague;
        public TeamMatch.Leagues SelectedLeague
        {
            get => _selectedLeague;
            set
            {
                if (SetProperty(ref _selectedLeague, value))
                {
                    SetLineupTemplate(_selectedLeague);
                    ReservesVisible = _selectedLeague == TeamMatch.Leagues.BadmintonLeague ||
                                      _selectedLeague == TeamMatch.Leagues.Division1;
                }
            }
        }

        private void SetLineupTemplate(TeamMatch.Leagues value)
        {
            var positions = new Dictionary<(Lineup.PositionType, int), PositionError>();
            var template = Lineup.LeaguePositions[value];
            foreach (var group in template)
            {
                for (int i = 0; i < group.Value; i++)
                {
                    positions.Add((group.Key, i), new PositionError());
                }
            }
            Positions = positions;
        }

        private Dictionary<(Lineup.PositionType, int), PositionError> _positions;

        public Dictionary<(Lineup.PositionType, int), PositionError> Positions
        {
            get => _positions;
            set
            {
                if (SetProperty(ref _positions, value))
                {
                    LineupHeight = _positions.Count * 150;
                }
            }
        }

        private ObservableCollection<Player> _players;
        public ObservableCollection<Player> Players
        {
            get => _players;
            set => SetProperty(ref _players, value);
        }

        private ObservableCollection<Member> _members;
        public ObservableCollection<Member> Members
        {
            get => _members;
            set => SetProperty(ref _members, value);
        }

        private readonly bool _isEdit = false;
        private readonly int _matchId = 0;

        //Ctor
        private CreateMatchViewModel(RequestCreator requestCreator, INavigation navigation) : base(requestCreator, navigation)
        {
            Members = new ObservableCollection<Member>(RequestCreator.GetAllMembers().OrderBy(p => p.Name));
            Players = new ObservableCollection<Player>(RequestCreator.GetAllPlayers().OrderBy(p => p.Member.Name));
            Players.ToList().RemoveAll(p => !p.OnRankList);
            SelectedLeague = TeamMatch.Leagues.DenmarksSeries;
        }

        public CreateMatchViewModel(DateTime startDate, RequestCreator requestCreator, INavigation navigation) : this(requestCreator, navigation)
        {
            SelectedDateStart = startDate;
            Location = "Stjernevej 5, 9200 Aalborg";
        }

        public CreateMatchViewModel(TeamMatch match, RequestCreator requestCreator, INavigation navigation) : this(requestCreator, navigation)
        {
            OpponentName = match.OpponentName;
            SelectedDateStart = match.Start.Date;
            SelectedTimeStart = match.Start.TimeOfDay;
            SelectedTimeEnd = match.End.TimeOfDay;
            Location = match.Location;
            SelectedLeague = match.League;
            LeagueRound = match.LeagueRound;
            Season = match.Season;
            TeamIndex = match.TeamIndex;

            _isEdit = true;
            _matchId = match.Id;
        }

        public void SetUILineup(Lineup lineup)
        {
            foreach (var group in lineup)
            {
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    Positions[(group.Type, i)] = new PositionError(group.Positions[i]);
                }
            }
        }

        private void RemoveSamePlayerDouble(Lineup lineup)
        {
            foreach (var group in lineup)
            {
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    var pos = group.Positions[i];
                    if (Lineup.PositionType.Double.HasFlag(group.Type) && pos.Player != null && pos.OtherPlayer != null &&
                        pos.Player.Member.Id == group.Positions[i].OtherPlayer.Member.Id)
                    {
                        pos.OtherPlayer = null;
                        pos.OtherIsExtra = false;
                    }
                }
            }

        }

        private RelayCommand _verifyLineupCommand;

        public RelayCommand VerifyLineupCommand
        {
            get
            {
                return _verifyLineupCommand ?? (_verifyLineupCommand = new RelayCommand(param => ExecuteVerifyLineup(param), param => CanExecuteVerifyLineup(param)));
            }
        }

        private bool CanExecuteVerifyLineup(object param)
        {
            return LeagueRound != null && LeagueRound > 0 && 
                   Season != null && Season > 0 && 
                   TeamIndex != null && TeamIndex > 0;
        }

        private void ExecuteVerifyLineup(object param)
        {
            TeamMatch match = new TeamMatch()
            { 
                Season = (int)Season,
                LeagueRound = (int)LeagueRound,
                TeamIndex = (int)TeamIndex,
                League = SelectedLeague,
                Lineup = ConvertPositionDictionaryToLineup(Positions)
            };
            if (_matchId != 0) 
                match.Id = _matchId;
            
            List<RuleBreak> ruleBreaks = RequestCreator.VerifyLineup(match);

            VerifyButtonColor = ruleBreaks.Count > 0 ? Color.Red : Color.Green;

            foreach (var position in Positions)
            {
                var posRuleBreak = ruleBreaks.Find(r => r.Position == position.Key && r.PositionIndex == 0);
                position.Value.Error = posRuleBreak == null ? string.Empty : posRuleBreak.ErrorMessage;

                if (Lineup.PositionType.Double.HasFlag(position.Key.Item1))
                {
                    var pos2RuleBreak = ruleBreaks.Find(r => r.Position == position.Key && r.PositionIndex == 1);
                    position.Value.OtherError = pos2RuleBreak == null ? string.Empty : pos2RuleBreak.ErrorMessage;
                }
            }
        }

        private Lineup ConvertPositionDictionaryToLineup(Dictionary<(Lineup.PositionType type, int index), PositionError> positions)
        {
            var lineup = new Lineup();
            Lineup.PositionType prevType = positions.First().Key.type;

            List<PositionError> pos = new List<PositionError>();

            foreach (var position in positions)
            {
                if (prevType != position.Key.type)
                {
                    lineup.Add( new Lineup.Group() {Type = prevType, Positions = PositionErrorsToPositions(pos)} );
                    pos = new List<PositionError>();
                }
                pos.Add(position.Value);
                prevType = position.Key.type;
            }
            lineup.Add(new Lineup.Group() { Type = prevType, Positions = PositionErrorsToPositions(pos)});
            return lineup;
        }

        private List<Position> PositionErrorsToPositions(List<PositionError> posErrors)
        {
            List<Position> pos = new List<Position>();
            foreach(var posError in posErrors)
                pos.Add(new Position() {IsExtra = posError.IsExtra, Player = posError.Player, OtherIsExtra = posError.OtherIsExtra, OtherPlayer = posError.OtherPlayer});
            return pos;
        }

        private RelayCommand _selectSinglePlayerCommand;

        public RelayCommand SelectSinglePlayerCommand
        {
            get
            {
                return _selectSinglePlayerCommand ?? (_selectSinglePlayerCommand = new RelayCommand(param => ExecuteSelectPlayerCommand(param, 0)));
            }
        }

        private RelayCommand _selectDoublePlayerCommand;

        public RelayCommand SelectDoublePlayerCommand
        {
            get
            {
                return _selectDoublePlayerCommand ?? (_selectDoublePlayerCommand = new RelayCommand(param => ExecuteSelectPlayerCommand(param, 1)));
            }
        }


        private void ExecuteSelectPlayerCommand(object param, int index)
        {
            var pos = ((Lineup.PositionType, int))param;

            ChooseLineupPlayerPopupPage page = new ChooseLineupPlayerPopupPage(Players.ToList(), RequestCreator);
            page.CallBackEvent += (sender, e) => SetChosenPlayer(sender, e, pos, index);
            PopupNavigation.Instance.PushAsync(page);
        }

        private void SetChosenPlayer(object sender, Player e, (Lineup.PositionType, int) pos, int index)
        {
            var newPositions = new Dictionary<(Lineup.PositionType, int), PositionError>(Positions);
            foreach (var p in newPositions)
            {
                if (p.Key.Item1 == pos.Item1 && p.Key.Item2 == pos.Item2)
                {
                    if (index == 0)
                        p.Value.Player = e;
                    else 
                        p.Value.OtherPlayer = e;
                }
            }

            Positions = newPositions;
        }

        private double _saveButtonOpacity;

        public double SaveButtonOpacity
        {
            get => _saveButtonOpacity; 
            set => SetProperty(ref _saveButtonOpacity, value); 
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
            if (((string.IsNullOrEmpty(Location)) ||
                  (string.IsNullOrEmpty(OpponentName)) ||
                  LeagueRound == null || LeagueRound < 0 ||
                  Season == null || Season < 0 ||
                  TeamIndex == null || TeamIndex < 0))
            {
                SaveButtonOpacity = 0.5;
                return false;
            }

            SaveButtonOpacity = 1;
            return true;
        }

        private void ExecuteSaveMatchClick(object param)
        {
            if (ValidateUserInput())
            {
                TeamMatch match = new TeamMatch()
                {
                    Captain = Captain,
                    Start = SelectedDateStart.Date + Convert.ToDateTime(SelectedTimeStart.ToString()).TimeOfDay,
                    End = SelectedDateStart.Date + Convert.ToDateTime(SelectedTimeEnd.ToString()).TimeOfDay,
                    League = SelectedLeague,
                    Lineup = ConvertPositionDictionaryToLineup(Positions),
                    LeagueRound = (int)LeagueRound,
                    Location = Location,
                    OpponentName = OpponentName,
                    Season = (int)Season,
                    TeamIndex = (int)TeamIndex
                };
                RemoveSamePlayerDouble(match.Lineup);

                if (_isEdit)
                    RequestCreator.DeleteTeamMatch(_matchId);
                RequestCreator.SetTeamMatch(match);

                //Navigate back
                Navigation.PopAsync();
            }
        }

        private bool ValidateUserInput()
        {
            if (LeagueRound < 0 || LeagueRound > 100)
            {
                Application.Current.MainPage.DisplayAlert("Invalid input", "League round must be between 0 and 100", "Ok");
                return false;
            }
            if (TeamIndex < 0 || TeamIndex > 100)
            {
                Application.Current.MainPage.DisplayAlert("Invalid input", "Team index must be between 0 and 100", "Ok");
                return false;
            }
            if (Season < 2000 || Season > 3000)
            {
                Application.Current.MainPage.DisplayAlert("Invalid input", "Season must be between 2000 and 3000", "Ok");
                return false;
            }
            if (Location.Length > 256)
            {
                Application.Current.MainPage.DisplayAlert("Invalid input", "Location can not contain more than 256 characters", "Ok");
                return false;
            }
            if (OpponentName.Length > 64)
            {
                Application.Current.MainPage.DisplayAlert("Invalid input", "Opponent name can not contain more than 64 characters", "Ok");
                return false;
            }
            return true;
        }
    }
}
