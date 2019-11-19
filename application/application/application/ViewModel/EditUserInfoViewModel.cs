using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.ViewModel
{
    class EditUserInfoViewModel : BaseViewModel
    {
        public Member Member { get; set; }
        public EditUserInfoViewModel(Member member)
        {
            Member = member;
        }
    }
}
