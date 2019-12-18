using application.ViewModel;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.SystemInterface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PracticeTeamPage 
    {
        private PracticeTeamViewModel _vm;
        public PracticeTeamPage(int id, RequestCreator requestCreator) : base(requestCreator)
        {
            InitializeComponent();
            _vm = new PracticeTeamViewModel(id, requestCreator, Navigation);
            BindingContext = _vm;

            var trainerNameTap = new TapGestureRecognizer();
            trainerNameTap.Tapped += (s, r) => TrainerNameClick();

            SwapPerson.Source = ImageSource.FromResource("application.Images.swapperson.png");
            AddPlayerButton.Source = ImageSource.FromResource("application.Images.add.png");
        }

        private void TrainerNameClick()
        {
            _vm.TrainerViewCommand.Execute(new object());
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var player = (Common.Model.Player)e.SelectedItem;
            if (player != null)
            {
                Navigation.PushAsync(new ProfilePage(player.Member.Id, RequestCreator));
                PlayerList.SelectedItem = null;
            }
        }
    }
}