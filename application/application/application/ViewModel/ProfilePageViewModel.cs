using System;
using System.Collections.Generic;
using System.Text;
using application.Model;

namespace application.ViewModel
{
    class ProfilePageViewModel : BaseViewModel
    {
        public Member CurrentMember { get; set; } = new Member();
        
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }
        public ProfilePageViewModel() 
        {
            CurrentMember.Name = "Pernille Pedersen";
            Name = CurrentMember.Name;
        }
    }
}
