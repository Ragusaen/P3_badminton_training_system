using application.ViewModel;
using Common.Model;
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
    public partial class PracticeTeamPage : ContentPage
    {
        private PracticeTeamViewModel _vm;
        public PracticeTeamPage(PracticeTeam practiceTeam)
        {
            InitializeComponent();
            _vm = new PracticeTeamViewModel(practiceTeam);
            _vm.Navigation = Navigation;
            BindingContext = _vm;

            var trainerTap = new TapGestureRecognizer();
            trainerTap.Tapped += (s,r) => TrainerClick();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var player = (Common.Model.Player)e.SelectedItem;
            Navigation.PushAsync(new ProfilePage(player.Member.Id));
            PlayerList.SelectedItem = null;
        }



        private void TrainerClick()
        {
            Navigation.PushAsync(new ProfilePage(_vm.Trainer.Member.Id));
        }
    }
}