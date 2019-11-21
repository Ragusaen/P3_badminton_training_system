using application.ViewModel;
using Common.Model;
using Rg.Plugins.Popup.Pages;
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
    public partial class ExercisePopupPage : PopupPage
    {
        public ExercisePopupPage(PracticeSession practice)
        {
            InitializeComponent();
            ExercisePopupViewModel vm = new ExercisePopupViewModel(practice);
            BindingContext = vm;
        }
    }
}