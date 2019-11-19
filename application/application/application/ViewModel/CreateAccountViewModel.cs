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

namespace application.ViewModel
{
    class CreateAccountViewModel : BaseViewModel
    {

        public CreateAccountViewModel()
        {
            _availablePlayers = RequestCreator.GetPlayersWithNoAccount();
        }

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

        private string _confirmationOfPasswordText;

        public string ConfirmationOfPasswordText
        {
            get { return _confirmationOfPasswordText; }
            set
            {
                if (SetProperty(ref _confirmationOfPasswordText, value))
                {
                    if (Password == ConfirmPassword)
                        _confirmationOfPasswordText = " ";
                    else if (Password != ConfirmPassword)
                        _confirmationOfPasswordText = "The two password you have enter are not identical";
                }

            }
        }

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
            return !(string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username)) && ConfirmPassword == Password;
        }

        private void ExecuteCreateAccountClick(object param)
        {
            Navigation.PushAsync(new ProfilePage());
        }

        private List<Player> _availablePlayers;

        private ObservableCollection<Player> _shownPlayerList;
        public ObservableCollection<Player> ShownPlayerList
        {
            get => _shownPlayerList;
            set => SetProperty(ref _shownPlayerList, value);
        }

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                if (!IsOnList)
                    UpdatePlayerList();
            }
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
                    .OrderByDescending(p => StringSearch.LongestCommonSubsequence(p.Member.Name, SearchText))
                    .Take(5)
                    .ToList()
            );
        }

        private bool _isOnList;

        public bool IsOnList
        {
            get => _isOnList;
            set
            {
                SetProperty(ref _isOnList, value);
            }
        }


        private int _selectedBadmintonId;

        public void PlayerSelected(Player player)
        {
            _selectedBadmintonId = player.BadmintonPlayerId;
        }
    }
}
