using application.Controller;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace application.ViewModel
{
    class FocusPointPopupViewModel : BaseViewModel
    {
        public Member User { get; set; }

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
                FocusPoints.OrderByDescending((x => StringSearch.longestCommonSubsequence(x.Descriptor.Name.ToLower(), SearchText.ToLower()))).ThenBy(x => x.Descriptor.Name.Length).ToList();
            }
        }

        private ObservableCollection<FocusPointItem> _focusPoints;

        public ObservableCollection<FocusPointItem> FocusPoints
        {
            get { return _focusPoints; }
            set
            {
                SetProperty(ref _focusPoints, value);
            }
        }


        public FocusPointPopupViewModel(Member user)
        {
            User = user;

            //TODO: GET ALL FOCUSPOITNS -> Remove ones user already has
            var list = new List<FocusPointItem>();
            list.Add(new FocusPointItem() { Descriptor = new FocusPointDescriptor() { Name = "Slag 1", Id=1 } });
            list.Add(new FocusPointItem() { Descriptor = new FocusPointDescriptor() { Name = "Svip Serv", Id=2 } });
            list.Add(new FocusPointItem() { Descriptor = new FocusPointDescriptor() { Name = "Slag 2", Id=3 } });
            list.Add(new FocusPointItem() { Descriptor = new FocusPointDescriptor() { Name = "Flad Serv", Id=4 } });
            list.Add(new FocusPointItem() { Descriptor = new FocusPointDescriptor() { Name = "Slag 3", Id=5 } });
            list.Add(new FocusPointItem() { Descriptor = new FocusPointDescriptor() { Name = "Serv", Id=6 } });

            list = list.Where(p => user.FocusPoints.All(q => q.Descriptor.Id != p.Descriptor.Id)).ToList();
            FocusPoints = new ObservableCollection<FocusPointItem>(list);
        }
    }
}
