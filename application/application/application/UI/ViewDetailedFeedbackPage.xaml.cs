using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewDetailedFeedbackPage : ContentPage
    {
      
        public ViewDetailedFeedbackPage()
        {
            InitializeComponent();
            ViewDetailedFeedbackViewModel vm = new ViewDetailedFeedbackViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;
        }
     }
}