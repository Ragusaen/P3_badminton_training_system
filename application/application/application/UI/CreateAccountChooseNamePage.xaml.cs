using application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccountChooseNamePage : ContentPage
    {
        public CreateAccountChooseNamePage()
        {
            InitializeComponent();
            CreateAccountChooseNameViewModel vm = new CreateAccountChooseNameViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;
        }
    }
}