using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.SystemInterface;
using application.ViewModel;
using Common.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateFocusPointPopupPage : PopupPage
    {
        private CreateFocusPointPopupViewModel vm;
        public CreateFocusPointPopupPage(bool canCreatePrivateFocusPoint)
        {
            InitializeComponent();
            vm = new CreateFocusPointPopupViewModel(canCreatePrivateFocusPoint);
            BindingContext = vm;
        }
        public CreateFocusPointPopupPage(bool canCreatePrivateFocusPoint, FocusPointDescriptor fp)
        {
            InitializeComponent();
            vm = new CreateFocusPointPopupViewModel(canCreatePrivateFocusPoint, fp);
            BindingContext = vm;
        }
    }
}