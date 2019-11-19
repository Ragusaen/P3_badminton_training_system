using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.ViewModel
{
    class EditUserInfoViewModel : BaseViewModel
    {
        public Member Member { get; set; }
        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
            }
        }
        private string _newPassword;

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                SetProperty(ref _newPassword, value);
            }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                SetProperty(ref _username, value);
            }
        }
        public EditUserInfoViewModel(Member member)
        {
            Member = member;
        }
        private RelayCommand _saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(param => SaveClick(param)));
            }
        }
        private async void SaveClick(object param)
        {
            
        }
    }
}
