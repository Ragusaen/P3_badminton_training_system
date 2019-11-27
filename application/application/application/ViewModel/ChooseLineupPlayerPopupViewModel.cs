using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using application.Controller;
using application.SystemInterface;
using Common.Model;

namespace application.ViewModel
{
    class ChooseLineupPlayerPopupViewModel : BaseViewModel
    {
        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                if (string.IsNullOrEmpty(_searchText))
                    Players = new ObservableCollection<Player>(Players.OrderByDescending(p => p.Member.Name).ToList());
                else
                {
                    Players = new ObservableCollection<Player>(_players.OrderByDescending(
                            x => StringExtension.LongestCommonSubsequence(x.Member.Name.ToLower(), SearchText.ToLower()))
                        .ThenBy(x => x.Member.Name.Length).ToList());
                }
            }
        }

        private ObservableCollection<Player> _players;
        public ObservableCollection<Player> Players
        {
            get => _players;
            set => SetProperty(ref _players, value);
        }

        public ChooseLineupPlayerPopupViewModel()
        {
            Players = new ObservableCollection<Player>(RequestCreator.GetAllPlayers());
            SearchText = null;
        }
    }
}
