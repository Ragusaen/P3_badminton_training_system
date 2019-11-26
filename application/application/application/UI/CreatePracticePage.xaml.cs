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
    public partial class CreatePracticePage : ContentPage
    {
        CreatePracticeViewModel vm;
        public CreatePracticePage() : this(DateTime.Today)
        {

        }

        public CreatePracticePage(DateTime time)
        {
            if (time < DateTime.Today)
                time = DateTime.Today;

            InitializeComponent();
            vm = new CreatePracticeViewModel(time);
            BindingContext = vm;
            vm.Navigation = Navigation;

            SaveIcon.Source = ImageSource.FromResource("application.Images.saveicon.png");
            BullsEyeIcon.Source = ImageSource.FromResource("application.Images.bullseyeicon.png");
            //DeleteIcon.Source = ImageSource.FromResource("application.Images.deleteicon.png");
        }
    }
}