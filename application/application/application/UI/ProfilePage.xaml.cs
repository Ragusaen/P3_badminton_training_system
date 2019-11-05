﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            ProfilePageViewModel vm = new ProfilePageViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;

            Settingsicon.Source = ImageSource.FromResource("application.Images.settingsicon.jpg");
        }
    }
}