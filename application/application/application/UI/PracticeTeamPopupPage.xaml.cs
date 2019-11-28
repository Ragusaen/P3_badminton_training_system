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
    public partial class PracticeTeamPopupPage : PopupPage
    {
        public PracticeTeamPopupPage(List<PracticeTeam> practiceTeams)
        {
            InitializeComponent();
            PracticeTeamPopupViewModel vm = new PracticeTeamPopupViewModel(practiceTeams);
            BindingContext = vm;
        }

        async void Dismiss(object sender, EventArgs args)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public event EventHandler<PracticeTeam> CallBackEvent;

        private void ListView_ItemSelectedPracticeTeam(object sender, SelectedItemChangedEventArgs e)
        {
            CallBackEvent?.Invoke(this, e.SelectedItem as PracticeTeam);
            PopupNavigation.Instance.PopAsync();
        }
    }
}