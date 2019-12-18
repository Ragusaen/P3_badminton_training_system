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
        private bool isEdit = false;
        public FocusPointDescriptor FocusPoint { get; set; }
        public CreateFocusPointPopupViewModel(bool canCreatePrivateFocusPoint, RequestCreator requestCreator, INavigation navigation) : base(requestCreator, navigation)
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


        public CreateFocusPointPopupViewModel(bool canCreatePrivateFocusPoint, FocusPointDescriptor fp, RequestCreator requestCreator, INavigation navigation) : this(canCreatePrivateFocusPoint, requestCreator, navigation)
        {
            FocusPoint = new FocusPointDescriptor
            {
                Name = fp.Name,
                Id = fp.Id,
                Description = fp.Description,
                VideoURL = fp.VideoURL,
            };

            isEdit = true;
        }
        
        private RelayCommand _createFocusPointCommand;

        public RelayCommand CreateFocusPointCommand => _createFocusPointCommand ?? (_createFocusPointCommand = new RelayCommand(CreateFocusPointClick));

        private void CreateFocusPointClick(object param)
        {
            if (!string.IsNullOrEmpty(FocusPoint.Name))
            {
                if (ValidateUserInput())
                {
                    if (isEdit)
                        RequestCreator.EditFocusPoint(FocusPoint);
                    else
                        FocusPoint = RequestCreator.CreateFocusPointDescriptor(FocusPoint);
                    CallBackEvent?.Invoke(this, FocusPoint);
                    PopupNavigation.Instance.PopAsync();
                }
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Please enter a name", "Ok");
            }
        }

        private bool ValidateUserInput()
        {
            if (FocusPoint.Name.Length > 64)
            {
                Application.Current.MainPage.DisplayAlert("Invalid input", "Name can not contain more than 64 characters", "Ok");
                return false;
            }
            if (FocusPoint.Description != null && FocusPoint.Description.Length > 1024)
            {
                Application.Current.MainPage.DisplayAlert("Invalid input", "Description can not contain more than 1024 characters", "Ok");
                return false;
            }
            if (FocusPoint.VideoURL != null && FocusPoint.VideoURL.Length > 256)
            {
                Application.Current.MainPage.DisplayAlert("Invalid input", "Video link can not contain more than 256 characters", "Ok");
                return false;
            }
            return true;
        }

        public event EventHandler<FocusPointDescriptor> CallBackEvent;
    }
}
