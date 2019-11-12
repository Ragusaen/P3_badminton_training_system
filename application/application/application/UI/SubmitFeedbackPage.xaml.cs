using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubmitFeedbackPage : ContentPage
    {
        public SubmitFeedbackPage()
        {
            InitializeComponent();
            SubmitFeedbackViewModel vm = new SubmitFeedbackViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;
        }
    }
}