using application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.SystemInterface;
using Common.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateMatchPage 
    {
        private CreateMatchViewModel _vm;

        //Future works
        public CreateMatchPage(RequestCreator requestCreator) : this(DateTime.Today, requestCreator)
        {

        }

        //Sets time to the selected date if its after current time
        public CreateMatchPage(DateTime time, RequestCreator requestCreator) : base(requestCreator)
        {
            if (time < DateTime.Today)
                time = DateTime.Today;

            Init(()=> new CreateMatchViewModel(time, requestCreator, Navigation));
        }

        //Edit mode for team match 
        public CreateMatchPage(TeamMatch teamMatch, RequestCreator requestCreator) : base(requestCreator)
        {
            Init(() => new CreateMatchViewModel(teamMatch, requestCreator, Navigation));

            _vm.SetUILineup(teamMatch.Lineup);
            if(teamMatch.Captain != null)
                CaptainPicker.SelectedItem = _vm.Members.FirstOrDefault(m => m.Id == teamMatch.Captain.Id);
        }

        //Sets BindingContext ViewModel and a image
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