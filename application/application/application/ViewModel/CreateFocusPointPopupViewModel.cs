using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using application.SystemInterface;
using Rg.Plugins.Popup.Services;

namespace application.ViewModel
{
    class CreateFocusPointPopupViewModel : BaseViewModel
    {
        public bool IsPrivateChecked { get; set; }
        public bool PrivateCheckBoxIsVisible { get; set; }
        public FocusPointDescriptor FocusPoint { get; set; }
        public CreateFocusPointPopupViewModel(bool canCreatePrivateFocusPoint)
        {
            if (canCreatePrivateFocusPoint)
            {
                if (RequestCreator.LoggedInMember.MemberType != MemberType.Trainer)
                {
                    PrivateCheckBoxIsVisible = false;
                    IsPrivateChecked = true;
                }
            }
            else
            {
                PrivateCheckBoxIsVisible = false;
                IsPrivateChecked = false;
            }

            FocusPoint = new FocusPointDescriptor {IsPrivate = IsPrivateChecked};
        }
        
        private RelayCommand _createFocusPointCommand;

        public RelayCommand CreateFocusPointCommand
        {
            get
            {
                return _createFocusPointCommand ?? (_createFocusPointCommand = new RelayCommand(param => CreateFocusPointClick(param)));
            }
        }
        private void CreateFocusPointClick(object param)
        {
            RequestCreator.CreateFocusPointDescriptor(FocusPoint);
            CallBackEvent?.Invoke(this, null);
            PopupNavigation.Instance.PopAsync();
        }

        public event EventHandler CallBackEvent;
    }
}
