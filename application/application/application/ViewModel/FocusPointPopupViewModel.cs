﻿using application.Controller;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using application.SystemInterface;
using application.UI;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace application.ViewModel
{
    class FocusPointPopupViewModel : BaseViewModel
    {
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                if (string.IsNullOrEmpty(_searchText))
                    FocusPoints = new ObservableCollection<FocusPointDescriptor>(FocusPoints.OrderBy(p => p.Name).ToList());
                else
                {
                    FocusPoints = new ObservableCollection<FocusPointDescriptor>(_shownFocusPoints.OrderByDescending(
                            x => StringExtension.LongestCommonSubsequence(x.Name.ToLower(),
                                SearchText.ToLower()))
                        .ThenBy(x => x.Name.Length).ToList());
                }
            }
        }

        public List<FocusPointItem> NotShownItems;
        private ObservableCollection<FocusPointDescriptor> _shownFocusPoints;
        public ObservableCollection<FocusPointDescriptor> FocusPoints
        {
            get => _shownFocusPoints;
            set => SetProperty(ref _shownFocusPoints, value);
        }

        public FocusPointPopupViewModel(List<FocusPointItem> focusPointItems, Player player, RequestCreator requestCreator, INavigation navigation) : base(requestCreator, navigation)
        {
            NotShownItems = focusPointItems;
            var list = RequestCreator.GetFocusPoints();
            list = list.Where(p => NotShownItems.All(q => q.Descriptor.Id != p.Id)).ToList();
            FocusPoints = new ObservableCollection<FocusPointDescriptor>(list);
            SearchText = null;
        }

        private RelayCommand _createNewFocusPointDescriptorClick;

        public RelayCommand CreateNewFocusPointDescriptorClick
        {
            get
            {
                return _createNewFocusPointDescriptorClick ?? (_createNewFocusPointDescriptorClick = new RelayCommand(param => CreateNewFocusPointDescriptor(param)));
            }
        }

        private async void CreateNewFocusPointDescriptor(object param)
        {
            var newPage = new CreateFocusPointPopupPage(true, RequestCreator);
            await PopupNavigation.Instance.PushAsync(newPage);
            ((CreateFocusPointPopupViewModel) newPage.BindingContext).CallBackEvent += OnCallBackEvent;
        }

        private void OnCallBackEvent(object sender, FocusPointDescriptor e)
        {
            var list = RequestCreator.GetFocusPoints();
            list = list.Where(p => NotShownItems.All(q => q.Descriptor.Id != p.Id)).ToList();
            FocusPoints = new ObservableCollection<FocusPointDescriptor>(list);
            CallBackEvent?.Invoke(this, e);
            PopupNavigation.Instance.PopAllAsync();
        }

        public event EventHandler<FocusPointDescriptor> CallBackEvent;
        public void FocusPointSelected(FocusPointDescriptor fpd)
        {
            CallBackEvent?.Invoke(this, fpd);
            PopupNavigation.Instance.PopAsync();
        }
    }
}
