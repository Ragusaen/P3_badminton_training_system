using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using application.SystemInterface;
using application.UI;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

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
                if (RequestCreator.LoggedInMember.MemberType.HasFlag(MemberType.Trainer))
                {
                    PrivateCheckBoxIsVisible = true;
                    IsPrivateChecked = false;
                }
                else
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
            if (FocusPoint.Name != null)
            {
                FocusPoint = RequestCreator.CreateFocusPointDescriptor(FocusPoint);
                CallBackEvent?.Invoke(this, FocusPoint);
                PopupNavigation.Instance.PopAsync();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Please enter a name", "OK");
            }
        }

        public event EventHandler<FocusPointDescriptor> CallBackEvent;
    }
}
