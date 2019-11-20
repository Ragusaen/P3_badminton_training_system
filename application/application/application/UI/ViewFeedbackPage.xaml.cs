using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.ViewModel;
using Common.Model;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewFeedbackPage : ContentPage
    {


        public ViewFeedbackPage(Player player)
        {
            InitializeComponent();
            ViewFeedbackViewModel vm = new ViewFeedbackViewModel(player);
            BindingContext = vm;
            vm.Navigation = Navigation;
        }

    }
}