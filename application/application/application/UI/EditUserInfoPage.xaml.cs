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
        private EditUserInfoViewModel _vm;

        public EditUserInfoPage(Member member)
        {
            InitializeComponent();
            _vm = new EditUserInfoViewModel(member);
            BindingContext = _vm;
            _vm.Navigation = Navigation;

            SaveIcon.Source = ImageSource.FromResource("application.Images.saveicon.png");
        }

        private void ShownOnlyRelevantInfo()
        {
            TrainerRelevant.IsVisible = _vm != null;
        }
    }
}