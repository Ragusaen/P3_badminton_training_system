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
    public partial class ViewPracticePage : ContentPage
    {
        public ViewPracticePage()
        {
            InitializeComponent();
            CreatePracticeViewModel vm = new CreatePracticeViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;

            EditIcon.Source = ImageSource.FromResource("application.Images.editicon2.png");
            BullsEyeIcon.Source = ImageSource.FromResource("application.Images.bullseyeicon.png");
        }
    }
}