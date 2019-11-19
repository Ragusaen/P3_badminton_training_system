using application.ViewModel;
using Common.Model;
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
    public partial class EditUserInfoPage : ContentPage
    {
        public EditUserInfoPage(Member member)
        {
            InitializeComponent();
            EditUserInfoViewModel vm = new EditUserInfoViewModel(member);
            BindingContext = vm;
            vm.Navigation = Navigation;
        }
    }
}