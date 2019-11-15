using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using application.Controller;
using application.UI;
using Common.Model;
using Rg.Plugins.Popup.Services;

namespace application.ViewModel
{
    class ProfilePageViewModel : BaseViewModel
    {
        public Member User { get; set; }
       
        private List<PracticeTeam> _teams;

        public List<PracticeTeam> Teams
        {
            get { return _teams; }
            set
            {
                SetProperty(ref _teams, value);
                            TeamListHeight = Teams.Count * 45;
            }
        }

        private ObservableCollection<FocusPointItem> _focusPoints;

        public ObservableCollection<FocusPointItem> FocusPoints
        {
            get { return _focusPoints; }
            set { SetProperty(ref _focusPoints, value); }
        }

        private int _teamListHeight;

        public int TeamListHeight
        {
            get { return _teamListHeight; }
            set { SetProperty(ref _teamListHeight, value); }
        }

        private int _focusPointListHeight;

        public int FocusPointListHeight
        {
            get { return _focusPointListHeight; }
            set { SetProperty(ref _focusPointListHeight, value); }
        }

        public ProfilePageViewModel() 
        {
            User = new Member() { Name = "Pernille Pedersen" };
            User.FocusPoints = new List<FocusPointItem>() { new FocusPointItem() { Descriptor = new FocusPointDescriptor() { Name = "Slag 1", Id = 1 } } };
            FocusPoints = new ObservableCollection<FocusPointItem>(User.FocusPoints);
            FocusPointListHeight = FocusPoints.Count * 45;

            Teams = new List<PracticeTeam>();
            Teams.Add(new PracticeTeam() {Name = "U17"});
            Teams.Add(new PracticeTeam() { Name = "Senior" });
        }


        private RelayCommand _addFocusPointCommand;

        public RelayCommand AddFocusPointCommand
        {
            get 
            { 
                return _addFocusPointCommand ?? (_addFocusPointCommand = new RelayCommand(param => ExecuteAddFocusPoint(param))); 
            }
        }

        private void ExecuteAddFocusPoint(object param)
        {
            FocusPointPopupPage page = new FocusPointPopupPage(User);
            page.CallBackEvent += FocusPointPopupPageCallback;
            PopupNavigation.Instance.PushAsync(page);
        }

        private void FocusPointPopupPageCallback(object sender, FocusPointItem e)
        {
            //TODO: UPDATE MODEL
            FocusPoints.Add(e);
            FocusPointListHeight = FocusPoints.Count * 45;
        }

        private RelayCommand _profileSettingCommand;

        public RelayCommand ProfileSettingCommand
        {
            get
            {
                return _profileSettingCommand ?? (_profileSettingCommand = new RelayCommand(param => ExecuteProfileSettingTap(param)));
            }
        }

        //Check if user is in database. Navigate to main page.
        private void ExecuteProfileSettingTap(object param)
        {
            User.Name = "Hallo";
        }

        private RelayCommand _viewFeedbackCommand;

        public RelayCommand ViewFeedbackCommand
        {
            get
            {
                return _viewFeedbackCommand ?? (_viewFeedbackCommand = new RelayCommand(param => ExecuteViewFeedbackClick(param)));
            }
        }
        private void ExecuteViewFeedbackClick(object param)
        {
            
        }
    }
}
