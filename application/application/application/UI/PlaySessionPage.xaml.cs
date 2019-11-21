using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.ViewModel;
using Common.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaySessionPage : ContentPage
    {
        private PlaySessionViewModel _vm;

        public PlaySessionPage(PlaySession playSession)
        {
            InitializeComponent();
            _vm = new PlaySessionViewModel(playSession);
            BindingContext = _vm;
            _vm.Navigation = Navigation;

            Time.Text = _vm.PlaySession.Start.ToString("hh:mm") + " - " + _vm.PlaySession.End.ToString("hh:mm");
            Date.Text = _vm.PlaySession.Start.ToString("dddd, d MMMM");
            Location.Text = _vm.PlaySession.Location;

            if (_vm.TeamMatch != null)
            {
                Name.Text = _vm.TeamMatch.OpponentName;
                
                _vm.TeamMatchRelevant
            }
        }
    }
}