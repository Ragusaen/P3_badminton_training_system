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
    public partial class ChooseTrainerPopupPage : PopupPage
    {
        //Sets BindingContext ViewModel
        private ChooseTrainerPopupViewModel _vm;
        public ChooseTrainerPopupPage()
        {
            InitializeComponent();
            _vm = new ChooseTrainerPopupViewModel();
            BindingContext = _vm;
        }
        //Clickes on Trainer and returns the Trainer in CallBackEvent
        public event EventHandler<Trainer> CallBackEvent;
        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CallBackEvent?.Invoke(this, e.SelectedItem as Trainer);
            PopupNavigation.Instance.PopAsync();
        }
    }
}