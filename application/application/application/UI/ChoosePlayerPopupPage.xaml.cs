﻿using System;
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
    public partial class ChoosePlayerPopupPage
    {
        //Sets BindingContext ViewModel
        private ChoosePlayerPopupViewModel _vm;
        public ChoosePlayerPopupPage(List<Player> doNotShow, RequestCreator requestCreator) : base(requestCreator)
        {
            InitializeComponent();
            _vm = new ChoosePlayerPopupViewModel(doNotShow, requestCreator, Navigation);
            BindingContext = _vm;
        }

        //Cancel
        async void Dismiss(object sender, EventArgs args)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        //Clickes on Player and returns the Player in CallBackEvent
        public event EventHandler<Player> CallBackEvent;
        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CallBackEvent?.Invoke(this, e.SelectedItem as Player);
            PopupNavigation.Instance.PopAsync();
        }
    }
}