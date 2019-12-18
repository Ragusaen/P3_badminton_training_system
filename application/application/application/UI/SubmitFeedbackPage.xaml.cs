using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.SystemInterface;
using application.ViewModel;
using Common.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubmitFeedbackPage
    {
        public SubmitFeedbackPage(PlaySession playsession, RequestCreator requestCreator) : base(requestCreator)
        {
            InitializeComponent();

            //Sets BindingContext ViewModel
            SubmitFeedbackViewModel vm = new SubmitFeedbackViewModel(playsession, requestCreator, Navigation);
            BindingContext = vm;

            Slider1.Value = 0f;
            Slider2.Value = 0f;
            Slider3.Value = 0f;
            Slider4.Value = 0f;

            Quest1.Completed += (s, a) => Quest2.Focus();
            Quest2.Completed += (s, a) => Quest3.Focus();
            Quest3.Completed += (s, a) => Quest4.Focus();
            if (playsession is PracticeSession)
            {
                Label1.Text = "How ready did you feel to train today?";
                Label2.Text = "How was your effort today taking into account how ready you felt?";
                Label3.Text = "How were you challenged today in relation to the exercises?";
                Label4.Text = "To what extent were you absorbed by the training today?";
                Label5.Text = "What helped make the training good today?";
                Label6.Text = "Were there any issues with the training today?";
                Label7.Text = "What were the main focus points for you today?";
                Label8.Text = "How has your day been today?";
            }
            if (playsession is TeamMatch)
            {
                Label1.Text = "How ready did you feel play a match today?";
                Label2.Text = "How was your effort today taking into account how ready you felt?";
                Label3.Text = "How were you challenged today?";
                Label4.Text = "To what extent were you absorbed by the match today?";
                Label5.Text = "What helped make the match good today?";
                Label6.Text = "Were there any issues with the match today?";
                Label7.Text = "What were the main focus points for you today?";
                Label8.Text = "How has your day been today?";
            }
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