using application.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using application.SystemInterface;
using Common.Model;
using application.Controller;

namespace application.ViewModel
{
    class CreateAccountViewModel : BaseViewModel
    {
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (SetProperty(ref _userName, value))
                    CreateAccountClickCommand.RaiseCanExecuteChanged();
            }
        }

        private string _passWord;

        public string PassWord
        {
            get { return _passWord; }
            set
            {
                if (SetProperty(ref _passWord, value))
                    CreateAccountClickCommand.RaiseCanExecuteChanged();
            }
        }


        private RelayCommand _createAccountClickCommand;

        public RelayCommand CreateAccountClickCommand
        {
            get
            {
                return _createAccountClickCommand ?? (_createAccountClickCommand = new RelayCommand(param => ExecuteCreateAccountClick(param)));
            }
        }

        //Check if user is in database. Navigate to main page.
        private void ExecuteCreateAccountClick(object param)
        {
            Navigation.PushAsync(new ProfilePage());
        }


        private List<Player> _availablePlayers;
        public ObservableCollection<Player> ShownPlayerList { get; set; }

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                UpdatePlayerList();
            }
        }

        private void UpdatePlayerList()
        {
            if (_availablePlayers == null)
            {
                _availablePlayers = RequestCreator.GetPlayersWithNoAccount();
            }
            Debug.WriteLine("GOT PLAYERS!");

            ShownPlayerList = new ObservableCollection<Player>(
                _availablePlayers
                    .OrderByDescending(p => StringSearch.LongestCommonSubsequence(p.Member.Name, SearchText))
                    .Take(5)
                    .ToList()
            );
        }

    }
}
