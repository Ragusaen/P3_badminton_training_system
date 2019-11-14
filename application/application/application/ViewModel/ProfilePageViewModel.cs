using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using application.Controller;
using application.UI;
using Common.Model;
using Xamarin.Forms;

namespace application.ViewModel
{
    class ProfilePageViewModel : BaseViewModel
    {
        public ObservableCollection<FocusPointItem> FocusPoint;
        public Member CurrentMember { get; set; } = new Member() { Name = "Pernille Pedersen"};
       
        private ObservableCollection<PracticeTeam> _teams;

        public ObservableCollection<PracticeTeam> Teams
        {
            get { return _teams; }
            set
            {
                SetProperty(ref _teams, value);
            }
        }

        private string _searchtext;

        public string SearchText
        {
            get { return _searchtext; }
            set
            {
                SetProperty(ref _searchtext, value);
                SearchResultFocusPoints = new ObservableCollection<FocusPointItem>(SearchResultFocusPoints.OrderByDescending((x => StringSearch.longestCommonSubsequence(x.Descriptor.Name, SearchText))).ThenBy(x => x.Descriptor.Name.Length).ToList());
            }
        }

        private ObservableCollection<FocusPointItem> _searchResultFocusPoints;

        public ObservableCollection<FocusPointItem> SearchResultFocusPoints
        {
            get { return _searchResultFocusPoints; }
            set
            {
                SetProperty(ref _searchResultFocusPoints, value);
                    FocusPointListHeight = SearchResultFocusPoints.Count * 45;
            }
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
            Teams = new ObservableCollection<PracticeTeam>();
            Teams.Add(new PracticeTeam() {Name = "U17"});
            Teams.Add(new PracticeTeam() { Name = "Senior" });
            FocusPoint = new ObservableCollection<FocusPointItem>();
            FocusPoint.Add(new FocusPointItem() { Descriptor = new FocusPointDescriptor() { Name = "Slag" } });
            FocusPoint.Add(new FocusPointItem() { Descriptor = new FocusPointDescriptor() { Name = "Svip Serv"} });
            FocusPoint.Add(new FocusPointItem() { Descriptor = new FocusPointDescriptor() { Name = "Slag" } });
            FocusPoint.Add(new FocusPointItem() { Descriptor = new FocusPointDescriptor() { Name = "Flad Serv" } });
            FocusPoint.Add(new FocusPointItem() { Descriptor = new FocusPointDescriptor() { Name = "Slag" } });
            FocusPoint.Add(new FocusPointItem() { Descriptor = new FocusPointDescriptor() { Name = "Serv" } });
            SearchResultFocusPoints = FocusPoint;
            TeamListHeight = Teams.Count * 45;
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
            string action = await Application.Current.MainPage.DisplayActionSheet("Choose what you want to edit:", "Cancel", null, "Edit Personal Information", "Edit Rights");


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
            Navigation.PushAsync(new ViewFeedbackPage());
        }
    }
}
