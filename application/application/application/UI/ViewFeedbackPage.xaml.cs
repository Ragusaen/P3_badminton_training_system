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
        List<Entry> entries = new List<Entry>
        {
            new Entry (2)
            {
                Color = SKColor.Parse("#33ccff"),
                Label = "Dato"
            },

            new Entry(-1)
            {
                Color = SKColor.Parse("#ff3399"),
                Label = "Dato"
            },

             new Entry(0)
            {
                Color = SKColor.Parse("#0099ff"),
                Label = "Dato"
            }
        };

        public ViewFeedbackPage()
        {
            InitializeComponent();

            Slider1.Chart = new LineChart { Entries = entries, LineMode = LineMode.Straight, PointMode = PointMode.Square, LabelTextSize = 25, PointSize = 12 };

            ViewFeedbackViewModel vm = new ViewFeedbackViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;
        }
    }
}