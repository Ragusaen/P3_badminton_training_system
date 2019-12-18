using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using application.Controller;
using application.SystemInterface;
using Common.Model;
using System.Linq;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace application.ViewModel
{
    class ChooseTrainerPopupViewModel : BaseViewModel
    {
        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                if (string.IsNullOrEmpty(_searchText))
                    Trainers = new ObservableCollection<Trainer>(Trainers.OrderByDescending(p => p.Member.Name).ToList());
                else
                {
                    Trainers = new ObservableCollection<Trainer>(_trainers.OrderByDescending(
                            x => StringExtension.LongestCommonSubsequence(x.Member.Name.ToLower(), SearchText.ToLower()))
                        .ThenBy(x => x.Member.Name.Length).ToList());
                }
            }
        }

        private ObservableCollection<Trainer> _trainers;
        public ObservableCollection<Trainer> Trainers
        {
            get => _trainers;
            set => SetProperty(ref _trainers, value);
        }

        public ChooseTrainerPopupViewModel(RequestCreator requestCreator, INavigation navigation) : base(requestCreator, navigation)
        {
            Trainers = new ObservableCollection<Trainer>(RequestCreator.GetAllTrainers());
            SearchText = null;
        }

        public event EventHandler<Trainer> CallBackEvent;
        public void TrainerChosen(Trainer t)
        {
            CallBackEvent?.Invoke(this, t);
            PopupNavigation.Instance.PopAsync();
        }
    }
}
