using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.ViewModel
{
    class CreateFocusPointPopupViewModel : BaseViewModel
    {
        FocusPointDescriptor FocusPoint { get; set; }
        public CreateFocusPointPopupViewModel()
        {
            FocusPoint = new FocusPointDescriptor();
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
         
        }
    }
}
