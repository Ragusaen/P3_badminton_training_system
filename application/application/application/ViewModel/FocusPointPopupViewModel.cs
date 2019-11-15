using application.Controller;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using application.SystemInterface;

namespace application.ViewModel
{
    class FocusPointPopupViewModel : BaseViewModel
    {
        private string _searchtext;

        public string SearchText
        {
            get { return _searchtext; }
            set
            {
                SetProperty(ref _searchtext, value);
                /*if (string.IsNullOrEmpty(_searchtext))
                    FocusPoints.OrderByDescending(p => p.Descriptor.Name);
                else*/
                FocusPoints.OrderByDescending((x => StringSearch.longestCommonSubsequence(x.Name.ToLower(), SearchText.ToLower()))).ThenBy(x => x.Name.Length).ToList();
            }
        }

        private ObservableCollection<FocusPointDescriptor> _focusPoints;

        public ObservableCollection<FocusPointDescriptor> FocusPoints
        {
            get { return _focusPoints; }
            set
            {
                SetProperty(ref _focusPoints, value);
            }
        }


        public FocusPointPopupViewModel(Member member)
        {
            var list = RequestCreator.GetFocusPoints();
            list = list.Where(p => member.FocusPoints.All(q => q.Descriptor.Id != p.Id)).ToList();
            FocusPoints = new ObservableCollection<FocusPointDescriptor>(list);
        }
    }
}
