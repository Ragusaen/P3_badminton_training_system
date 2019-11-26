﻿using System;
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
    public partial class ChoosePlayerPopupPage : PopupPage
    {
        private ChoosePlayerPopupViewModel _vm;
        public ChoosePlayerPopupPage(List<Player> doNotShow)
        {
            InitializeComponent();
            _vm = new ChoosePlayerPopupViewModel(doNotShow);
            BindingContext = _vm;
        }

        public event EventHandler<Player> CallBackEvent;
        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CallBackEvent?.Invoke(this, e.SelectedItem as Player);
            PopupNavigation.Instance.PopAsync();
        }
    }
}