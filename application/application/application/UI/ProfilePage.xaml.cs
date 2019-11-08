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
    public partial class ProfilePage : ContentPage
    {
        List<Entry> entries = new List<Entry>
        {
            new Entry (2)
            {
                Color = SKColor.Parse("#33ccff"),
                Label = "Dato",
                ValueLabel = "5"

            },

            new Entry(-1)
            {
                Color = SKColor.Parse("#ff3399"),
                Label = "Dato",
                ValueLabel = "2"
            },

             new Entry(0)
            {
                Color = SKColor.Parse("#0099ff"),
                Label = "Dato",
                
               
                ValueLabel = "3"
            }
        };

        public ProfilePage()
        {
            InitializeComponent();

            Chart1.Chart = new LineChart { Entries = entries };

            
            ProfilePageViewModel vm = new ProfilePageViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;


            Settingsicon.Source = ImageSource.FromResource("application.Images.settingsicon.jpg");

            Settingsicon.Source = ImageSource.FromResource("application.Images.settingsicon.jpg");
        }
    }
}