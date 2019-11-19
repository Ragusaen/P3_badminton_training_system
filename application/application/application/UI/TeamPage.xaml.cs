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
    public partial class TeamPage : ContentPage
    {
        public TeamPage(PracticeTeam Team)
        {
            InitializeComponent();
            TeamViewModel vm = new TeamViewModel(Team);
            vm.Navigation = Navigation;
            BindingContext = vm;
        }
    }
}