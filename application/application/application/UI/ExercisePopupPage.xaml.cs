using application.ViewModel;
using Common.Model;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
        //Sets BindingContext ViewModel
        public ExercisePopupPage(PracticeSession practice)
        {
            InitializeComponent();
            ExercisePopupViewModel vm = new ExercisePopupViewModel(practice);
            BindingContext = vm;
        }

        //Cancel
        async void Dismiss(object sender, EventArgs args)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        //Clickes on Exercise and returns the Exercise in CallBackEvent
        public event EventHandler<ExerciseDescriptor> CallBackEvent;

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CallBackEvent?.Invoke(this, e.SelectedItem as ExerciseDescriptor);
            PopupNavigation.Instance.PopAsync();
        }
    }
}