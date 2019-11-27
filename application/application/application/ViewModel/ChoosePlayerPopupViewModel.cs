using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using application.Controller;
using application.SystemInterface;
using Common.Model;
using Rg.Plugins.Popup.Services;

namespace application.ViewModel
{
    class ChoosePlayerPopupViewModel : BaseViewModel
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

        private ObservableCollection<Player> _players;
        public ObservableCollection<Player> Players
        {
            get => _players;
            set => SetProperty(ref _players, value);
        }

        public ChoosePlayerPopupViewModel(List<Player> doNotShowPlayers)
        {
            var toRemove = doNotShowPlayers;
            var allPlayers = new ObservableCollection<Player>(RequestCreator.GetAllPlayers());
            var resPlayers = allPlayers.Where(p => toRemove.All(q => q.Member.Id != p.Member.Id)).ToList();
            Players = new ObservableCollection<Player>(resPlayers);
            SearchText = null;
        }

        public event EventHandler<Player> CallBackEvent;
        public void PlayerChosen(Player p)
        {
            CallBackEvent?.Invoke(this, p);
            PopupNavigation.Instance.PopAsync();
        }
    }
}
