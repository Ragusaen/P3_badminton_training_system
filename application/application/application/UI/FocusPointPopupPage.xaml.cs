using application.ViewModel;
using Common.Model;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FocusPointPopupPage : PopupPage
    {
        public FocusPointPopupPage(List<FocusPointItem> focusPointItems)
        {
            InitializeComponent();
            FocusPointPopupViewModel vm = new FocusPointPopupViewModel(focusPointItems);
            BindingContext = vm;
        }

        public event EventHandler<FocusPointDescriptor> CallBackEvent;

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CallBackEvent?.Invoke(this, e.SelectedItem as FocusPointDescriptor);
            PopupNavigation.Instance.PopAsync();
        }
    }
}