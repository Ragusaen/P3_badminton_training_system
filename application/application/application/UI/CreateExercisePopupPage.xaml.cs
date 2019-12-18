using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.SystemInterface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using application.ViewModel;
using Rg.Plugins.Popup.Services;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateExercisePopupPage 
    {
        //Sets BindingContext ViewModel
        public CreateExercisePopupPage(RequestCreator requestCreator) : base(requestCreator)
        {
            InitializeComponent();
            CreateExercisePopupViewModel vm = new CreateExercisePopupViewModel(requestCreator, Navigation);
            BindingContext = vm;
        }

        //Cancel
        async void Dismiss(object sender, EventArgs args)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}