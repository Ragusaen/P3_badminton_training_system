using application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateMatchPage : ContentPage
    {
        public CreateMatchPage() : this(DateTime.Today)
        {

        }

        public CreateMatchPage(DateTime time)
        {
            InitializeComponent();
            if (time < DateTime.Today)
                time = DateTime.Today;

            CreateMatchViewModel vm = new CreateMatchViewModel(time);
            BindingContext = vm;
            vm.Navigation = Navigation;
            SaveIcon.Source = ImageSource.FromResource("application.Images.saveicon.png");
        }

    }
}