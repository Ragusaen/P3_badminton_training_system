using application.ViewModel;
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
    public partial class MenuPage : MasterDetailPage
    {
        public List<MasterPageItem> MenuList { get; set; } 

        public MenuPage()
        {
            InitializeComponent();
            MenuList = new List<MasterPageItem>();

            MenuList.Add(new MasterPageItem() { Title = "Schedule", TargetType = typeof(SchedulePage)}); //Set icons
            MenuList.Add(new MasterPageItem() { Title = "Team", TargetType = typeof(TeamPage) }); //Set icons
            MenuList.Add(new MasterPageItem() { Title = "Profile", TargetType = typeof(ProfilePage)}); //Set icons

            NavigationList.ItemsSource = MenuList;

            //Navigate to Homepage
            Detail = new NavigationPage(new SchedulePage());
            IsPresented = false;
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (Detail as NavigationPage).PushAsync((Page)Activator.CreateInstance(((MasterPageItem)e.SelectedItem).TargetType));
            IsPresented = false;
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}