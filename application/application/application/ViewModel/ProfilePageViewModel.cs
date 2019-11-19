using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using application.Controller;
using application.UI;
using Common.Model;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace application.ViewModel
{
    class ProfilePageViewModel : BaseViewModel
    {
        public Member User { get; set; }
       
        private ObservableCollection<PracticeTeam> _teams;

        public ObservableCollection<PracticeTeam> Teams
        {
            get { return _teams; }
            set
            {
                SetProperty(ref _teams, value);
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
            User.FocusPoints = new List<FocusPointItem>() { new FocusPointItem() { Descriptor = new FocusPointDescriptor() { Name = "Slag 1", Id = 9999 } } };
            FocusPoints = new ObservableCollection<FocusPointItem>(User.FocusPoints);
            FocusPointListHeight = FocusPoints.Count * 45;

            Teams = new ObservableCollection<PracticeTeam>();
            Teams.Add(new PracticeTeam() { Name = "U17" });
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

        public void PopupFocusPoint(FocusPointItem focusPoint)
        {
            StringAndHeaderPopup popup = new StringAndHeaderPopup(focusPoint.Descriptor);
            PopupNavigation.Instance.PushAsync(popup);
        }


        private void FocusPointPopupPageCallback(object sender, FocusPointDescriptor e)
        {
            //TODO: UPDATE MODEL
            var item = new FocusPointItem
            {
                Descriptor = e,
                DateAssigned = DateTime.Now
            };
            User.FocusPoints.Add(item);
            FocusPoints.Add(item);
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
        private async void ExecuteProfileSettingTap(object param)
        {
            //Needs to change depending on user type

            string action = await Application.Current.MainPage.DisplayActionSheet("Choose what you want to edit:", "Cancel", null, "Edit User's Information", "Edit User's Rights");

            if (action == "Edit User's Information")
                await Navigation.PushAsync(new EditUserInfoPage());
            else if (action == "Edit User's Rights")
            {
                string rights = await Application.Current.MainPage.DisplayActionSheet("Choose user's rights:", "Cancel", null, "Player", "Trainer", "Player and Trainer");
            }
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
            Navigation.PushAsync(new ViewDetailedFeedbackPage(User));
        }

        private RelayCommand _viewFeedbackGraphCommand;

        public RelayCommand ViewFeedbackGraphCommand
        {
            get
            {
                return _viewFeedbackGraphCommand ?? (_viewFeedbackGraphCommand = new RelayCommand(param => ExecuteViewFeedbackGraphClick(param)));
            }
        }
        private void ExecuteViewFeedbackGraphClick(object param)
        {
            Navigation.PushAsync(new ViewFeedbackPage(User));
        }
        private RelayCommand _deleteListTeamItemCommand;

        public RelayCommand DeleteListTeamItemCommand
        {
            get
            {
                return _deleteListTeamItemCommand ?? (_deleteListTeamItemCommand = new RelayCommand(param => DeleteListTeamItemClick(param)));
            }
        }
        private void DeleteListTeamItemClick(object param)
        {
            PracticeTeam team = param as PracticeTeam;
            Teams.Remove(team);
            TeamListHeight = Teams.Count * 45; 
        }
        private RelayCommand _deleteListFocusItemCommand;

        public RelayCommand DeleteListFocusItemCommand
        {
            get
            {
                return _deleteListFocusItemCommand ?? (_deleteListFocusItemCommand = new RelayCommand(param => DeleteListFocusItemClick(param)));
            }
        }
        private void DeleteListFocusItemClick(object param)
        {
            FocusPointItem focuspoint = param as FocusPointItem;
            FocusPoints.Remove(focuspoint);
            FocusPointListHeight = FocusPoints.Count * 45;
        }
    }
}