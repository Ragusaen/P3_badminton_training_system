using application.ViewModel;
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
        public PracticeTeamPage()
        {
            InitializeComponent();
            PracticeTeamViewModel vm = new PracticeTeamViewModel();
            vm.Navigation = Navigation;
            BindingContext = vm;
        }
    }
}