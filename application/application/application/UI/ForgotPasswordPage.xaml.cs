using application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.SystemInterface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage
    {
        //Sets BindingContext ViewModel
        public ForgotPasswordPage(RequestCreator requestCreator) : base(requestCreator)
        {
            InitializeComponent();
            ForgotPasswordViewModel vm = new ForgotPasswordViewModel(requestCreator, Navigation);
            BindingContext = vm;
            vm.Navigation = Navigation;
        }
    }
}