using application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateMatchPage : ContentPage
    {
        private CreateMatchViewModel _vm;

        public CreateMatchPage() : this(DateTime.Today)
        {

        }

        public CreateMatchPage(DateTime time)
        {
            if (time < DateTime.Today)
                time = DateTime.Today;

            Init(()=> new CreateMatchViewModel(time));
        }

        public CreateMatchPage(TeamMatch teamMatch)
        {
            Init(() => new CreateMatchViewModel(teamMatch));

            _vm.SetUILineup(teamMatch.Lineup);
        }

        private void Init(Func<CreateMatchViewModel> ctor)
        {

            InitializeComponent();

            _vm = ctor();
            BindingContext = _vm;
            _vm.Navigation = Navigation;
            SaveIcon.Source = ImageSource.FromResource("application.Images.saveicon.png");
        }

    }
}