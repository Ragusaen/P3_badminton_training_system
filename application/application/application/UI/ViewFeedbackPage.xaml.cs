using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.ViewModel;
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


        public ViewFeedbackPage()
        {
            InitializeComponent();



            ViewFeedbackViewModel vm = new ViewFeedbackViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;
        }

    }
}