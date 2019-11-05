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
    public partial class CreatePracticePage : ContentPage
    {
        public CreatePracticePage()
        {
            InitializeComponent();

            CreatePracticeViewModel vm = new CreatePracticeViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;

            BullsEyeIcon.Source = ImageSource.FromResource("application.Images.bullseyeicon.png");
        }
    }
}