using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using application.Model;

namespace application.ViewModel
{
    class CreateTeamViewModel : BaseViewModel
    {
        public Member CurrentMember { get; set; } = new Member("Pernille Pedersen");

        private int _memberListHeight;

        public int MemberListHeight
        {
            get { return _memberListHeight; }
            set { SetProperty(ref _memberListHeight, value); }
        }

        private string _searchtext;

        public string SearchText
        {
            get { return _searchtext; }
            set
            {
                SetProperty(ref _searchtext, value);
            }
        }
        private ObservableCollection<Member> _searchResultMember;

        public ObservableCollection<Member> SearchResultMember
        {
            get { return _searchResultMember; }
            set
            {
                SetProperty(ref _searchResultMember, value);
            }
        }
        private List<string> _hej;

        public List<string> Hej
        {
            get { return _hej; }
            set
            {
                SetProperty(ref _hej, value);
            }
        }
        public CreateTeamViewModel()
        {
            SearchResultMember = new ObservableCollection<Member>();
            SearchResultMember.Add(new Member("Name"));
            SearchResultMember.Add(new Member("Name"));
            SearchResultMember.Add(new Member("Name"));
            SearchResultMember.Add(new Member("Name"));
            SearchResultMember.Add(new Member("Name"));
            SearchResultMember.Add(new Member("Name"));

        }
    }
}
