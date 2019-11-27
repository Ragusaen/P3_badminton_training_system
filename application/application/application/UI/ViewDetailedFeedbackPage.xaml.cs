using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ViewDetailedFeedbackPage : ContentPage
    {
      
        public ViewDetailedFeedbackPage(Player player)
        {
            InitializeComponent();
            ViewDetailedFeedbackViewModel vm = new ViewDetailedFeedbackViewModel(player);
            BindingContext = vm;
            vm.Navigation = Navigation;
        }
    }
}