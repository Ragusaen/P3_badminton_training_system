using application.Controller;
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

namespace application.ViewModel
{
    class FocusPointPopupViewModel : BaseViewModel
    {
        private string _searchText;

        public List<FocusPointItem> NotShownItems;

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                /*if (string.IsNullOrEmpty(_searchtext))
                    FocusPoints.OrderByDescending(p => p.Descriptor.Name);
                else*/
                FocusPoints.OrderByDescending((x => StringExtension.LongestCommonSubsequence(x.Name.ToLower(), SearchText.ToLower()))).ThenBy(x => x.Name.Length).ToList();
            }
        }

        private ObservableCollection<FocusPointDescriptor> _shownFocusPoints;
        public ObservableCollection<FocusPointDescriptor> FocusPoints
        {
            get => _shownFocusPoints;
            set => SetProperty(ref _shownFocusPoints, value);
        }

        public FocusPointPopupViewModel(List<FocusPointItem> focusPointItems)
        {
            NotShownItems = focusPointItems;
            var list = RequestCreator.GetFocusPoints();
            list = list.Where(p => NotShownItems.All(q => q.Descriptor.Id != p.Id)).ToList();
            FocusPoints = new ObservableCollection<FocusPointDescriptor>(list);
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
            var newPage = new CreateFocusPointPopupPage(true);
            await PopupNavigation.Instance.PushAsync(newPage);
            ((CreateFocusPointPopupViewModel) newPage.BindingContext).CallBackEvent += OnCallBackEvent;
        }

        private void OnCallBackEvent(object sender, EventArgs e)
        {
            var list = RequestCreator.GetFocusPoints();
            list = list.Where(p => NotShownItems.All(q => q.Descriptor.Id != p.Id)).ToList();
            FocusPoints = new ObservableCollection<FocusPointDescriptor>(list);
        }
    }
}
