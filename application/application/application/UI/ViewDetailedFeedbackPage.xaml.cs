using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewDetailedFeedbackPage : ContentPage
    {
      
        public ViewDetailedFeedbackPage()
        {
            InitializeComponent();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var feedback = e.Item as Feedback;
            var vm = BindingContext as ViewDetailedFeedbackViewModel;
            vm?.ShowOrHideFeedback(feedback);
        }

     }
}