using application.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using application.SystemInterface;
using Common.Model;
using application.Controller;
using Common.Serialization;

namespace application.ViewModel
{
    class CreateAccountViewModel : BaseViewModel
    {

        public CreateAccountViewModel()
        {
            SearchText = "";
            _availablePlayers = RequestCreator.GetPlayersWithNoAccount();
            UpdatePlayerList();
        }

        #region Properties
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (SetProperty(ref _username, value))
                    CreateAccountClickCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _usernameErrorVisibility;
        public bool UsernameErrorVisibility
        {
            get => _usernameErrorVisibility;
            set => SetProperty(ref _usernameErrorVisibility, value);
        }


        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (SetProperty(ref _password, value))
                    CreateAccountClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                if (SetProperty(ref _confirmPassword, value))
                    CreateAccountClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                if (!NotOnList)
                    UpdatePlayerList();
            }
        }

        private bool _notOnList;
        public bool NotOnList
        {
            get => _notOnList;
            set
            {
                if (SetProperty(ref _notOnList, value))
                    CreateAccountClickCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        private RelayCommand _createAccountClickCommand;

        public RelayCommand CreateAccountClickCommand
        {
            get
            {
                return _createAccountClickCommand ?? (_createAccountClickCommand = new RelayCommand(param => ExecuteCreateAccountClick(param), param => CanExecuteCreateAccountClick(param)));
            }
        }

        private bool CanExecuteCreateAccountClick(object param)
        {
            return !string.IsNullOrEmpty(Password)
                   && !string.IsNullOrEmpty(Username)
                   && ConfirmPassword == Password
                   && (_selectedBadmintonId != null || (NotOnList && !string.IsNullOrEmpty(SearchText)));
        }

        private void ExecuteCreateAccountClick(object param)
        {
            bool success;
            if (!NotOnList)
            {
                success = RequestCreator.CreateAccountRequest(Username, Password, _selectedBadmintonId.Value, null);
            }
            else
            {
                success = RequestCreator.CreateAccountRequest(Username, Password, 0, SearchText);
            }

            if (success)
                //Navigate back
                Navigation.PopAsync();
            else
            {
                UsernameErrorVisibility = true;
            }
        }

        private List<Player> _availablePlayers;

        private ObservableCollection<Player> _shownPlayerList;
        public ObservableCollection<Player> ShownPlayerList
        {
            get => _shownPlayerList;
            set => SetProperty(ref _shownPlayerList, value);
        }

        private void UpdatePlayerList()
        {
            if (_availablePlayers == null)
            {
                Debug.WriteLine("No players loaded");
                return;
            }

            ShownPlayerList = new ObservableCollection<Player>(
                _availablePlayers
                    .OrderByDescending(p => StringExtension.LongestCommonSubsequence(p.Member.Name.ToLower(), SearchText.ToLower()))
                    .Take(5)
                    .ToList()
            );
        }

        private int? _selectedBadmintonId = null;

        public void PlayerSelected(Player player)
        {
            _selectedBadmintonId = player.BadmintonPlayerId;
            CreateAccountClickCommand.RaiseCanExecuteChanged();
        }

        public void PlayerUnselect()
        {
            _selectedBadmintonId = null;
            CreateAccountClickCommand.RaiseCanExecuteChanged();
        }
    }
}
