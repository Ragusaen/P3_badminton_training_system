using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.ViewModel;
using Common.Model;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewFocusPointDetails : PopupPage
    {
        public ViewFocusPointDetails(FocusPointDescriptor focusPoint)
        {
            InitializeComponent();
            var vm = new ViewModel.ViewFocusPointDetailsViewModel(focusPoint);
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