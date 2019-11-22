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
        public PlaySessionPage(PlaySession playSession)
        {
            InitializeComponent();
            BindingContext = new PlaySessionViewModel(playSession);
        }
    }
}