using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.SystemInterface;
using application.ViewModel;
using Common.Model;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewFocusPointDetails
    {
        public ViewFocusPointDetails(FocusPointDescriptor focusPoint, RequestCreator requestCreator) : base(requestCreator)
        {
            //Sets BindingContext ViewModel
            InitializeComponent();
            var vm = new ViewFocusPointDetailsViewModel(focusPoint, requestCreator, Navigation);
            BindingContext = vm;

            UrlText.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(LinkClicked)
            });

            var tapper = new TapGestureRecognizer();
            tapper.Tapped += (s, a) => PopupNavigation.Instance.PopAsync();
            OuterStack.GestureRecognizers.Add(tapper);
        }

        async void Dismiss(object sender, EventArgs args)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private void LinkClicked()
        {
            Device.OpenUri(new System.Uri(UrlText.Text));
        }
    }
}