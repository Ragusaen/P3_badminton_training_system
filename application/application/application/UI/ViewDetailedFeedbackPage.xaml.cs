using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.SystemInterface;
using application.ViewModel;
using Common.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewDetailedFeedbackPage 
    {
        //Sets BindingContext ViewModel
        public ViewDetailedFeedbackPage(Player player, RequestCreator requestCreator) : base(requestCreator)
        {
            InitializeComponent();
            ViewDetailedFeedbackViewModel vm = new ViewDetailedFeedbackViewModel(player, requestCreator, Navigation);
            BindingContext = vm;
        }
    }
}