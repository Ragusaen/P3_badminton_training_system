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
    public partial class SubmitFeedbackPage : ContentPage
    {
        public SubmitFeedbackPage(PlaySession playsession)
        {
            InitializeComponent();
            SubmitFeedbackViewModel vm = new SubmitFeedbackViewModel(playsession);
            BindingContext = vm;
            vm.Navigation = Navigation;

            Slider1.Value = 0f;
            Slider2.Value = 0f;
            Slider3.Value = 0f;
            Slider4.Value = 0f;

            Quest1.Completed += (s, a) => Quest2.Focus();
            Quest2.Completed += (s, a) => Quest3.Focus();
            Quest3.Completed += (s, a) => Quest4.Focus();
            Quest4.Completed += (s, a) => SubmitButton.SendClicked();
        }

        private void Slider1_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (Slider1.Value != Math.Round(e.NewValue))
                Slider1.Value = Math.Round(e.NewValue);
        }

        private void Slider2_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (Slider2.Value != Math.Round(e.NewValue))
                Slider2.Value = Math.Round(e.NewValue);
        }

        private void Slider3_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (Slider3.Value != Math.Round(e.NewValue))
                Slider3.Value = Math.Round(e.NewValue);
        }

        private void Slider4_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (Slider4.Value != Math.Round(e.NewValue))
                Slider4.Value = Math.Round(e.NewValue);
        }
    }
}