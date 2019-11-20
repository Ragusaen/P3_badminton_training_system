using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.SystemInterface;
using application.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateFocusPointPopupPage : PopupPage
    {
        public CreateFocusPointPopupPage(bool canCreatePrivateFocusPoint)
        {
            InitializeComponent();
            var vm = new CreateFocusPointPopupViewModel(canCreatePrivateFocusPoint);
            BindingContext = vm;
        }
    }
}