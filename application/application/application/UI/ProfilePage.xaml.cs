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
            new Entry (5)
            {
                Color = SKColor.Parse("#33ccff"),
                Label = "Dato"},

            new Entry(2)
            {
                Color = SKColor.Parse("#ff3399"),
                Label = "Dato"},

             new Entry(3)
            {
                Color = SKColor.Parse("#0099ff"),
                Label = "Dato"}
        };

        
        public ProfilePage()
        {
            InitializeComponent();
            
            Chart1.Chart = new LineChart() { Entries = entries, LineMode = LineMode.Straight, LineSize = 8, PointMode = PointMode.Square, PointSize = 15, LabelTextSize = 45 };

            ProfilePageViewModel vm = new ProfilePageViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;


            Settingsicon.Source = ImageSource.FromResource("application.Images.settingsicon.jpg");

            //Sub page navigation:
            //(((MasterDetailPage)Application.Current.MainPage).Detail as NavigationPage).PushAsync(new SubmitFeedbackPage());
        }
    }
}