using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.ViewModel;
using Common.Model;
using Microcharts;
using Rg.Plugins.Popup.Services;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerProfilePage : ContentPage
    {
        private ProfilePageViewModel _profilePageViewModel;

        List<Microcharts.Entry> entries = new List<Microcharts.Entry>
        {
            new Microcharts.Entry (2)
            {
                Color = SKColor.Parse("#33ccff"),
                Label = "Dato"
            },

            new Microcharts.Entry(-1)
            {
                Color = SKColor.Parse("#ff3399"),
                Label = "Dato"
            },

             new Microcharts.Entry(0)
            {
                Color = SKColor.Parse("#0099ff"),
                Label = "Dato"
            }
        };

        public PlayerProfilePage(Member member)
        {
            InitializeComponent();

            FeedbackChart.Chart = new LineChart { Entries = entries, LineMode = LineMode.Straight, PointMode = PointMode.Square, LabelTextSize = 25, PointSize = 12};

            _profilePageViewModel = new ProfilePageViewModel(member);
            BindingContext = _profilePageViewModel;
            _profilePageViewModel.Navigation = Navigation;

            Settingsicon.Source = ImageSource.FromResource("application.Images.settingsicon.jpg");
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var focusPoint = (FocusPointItem) e.SelectedItem;
            if (focusPoint != null)
                _profilePageViewModel.PopupFocusPoint(focusPoint);
            FocusPointList.SelectedItem = null;
        }
    }
}