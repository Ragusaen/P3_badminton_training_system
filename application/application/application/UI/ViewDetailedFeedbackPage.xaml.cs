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
      
        public ViewDetailedFeedbackPage(Member member)
        {
            InitializeComponent();
            ViewDetailedFeedbackViewModel vm = new ViewDetailedFeedbackViewModel(member);
            BindingContext = vm;
            vm.Navigation = Navigation;
        }
     }
}