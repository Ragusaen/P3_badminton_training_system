using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.ViewModel;
using Common.Model;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StringAndHeaderPopup : PopupPage
    {
        public StringAndHeaderPopup(FocusPointDescriptor focusPoint)
        {
            InitializeComponent();
            var vm = new StringAndHeaderViewModel(focusPoint);
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