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
    public partial class EditPersonalInfoPage : ContentPage
    {
        public EditPersonalInfoPage()
        {
            InitializeComponent();
            EditPersonalInfoViewModel vm = new EditPersonalInfoViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;
        }
    }
}