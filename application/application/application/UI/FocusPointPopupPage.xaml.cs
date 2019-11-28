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

        public FocusPointPopupPage(List<FocusPointItem> focusPointItems, Player player)
        {
            InitializeComponent();
            FocusPointPopupViewModel vm = new FocusPointPopupViewModel(focusPointItems, player);
            BindingContext = vm;
        }

        async void Dismiss(object sender, EventArgs args)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((FocusPointPopupViewModel)BindingContext).FocusPointSelected(e.SelectedItem as FocusPointDescriptor);
        }
    }
}