using application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
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
            MenuViewModel vm = new MenuViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;

            MenuList = new List<MasterPageItem>();

            MenuList.Add(new MasterPageItem() { Title = "Schedule", TargetType = typeof(SchedulePage) }); //Set icons
            MenuList.Add(new MasterPageItem() { Title = "Team", TargetType = typeof(PracticeTeamPage) }); //Set icons
            MenuList.Add(new MasterPageItem() { Title = "Profile", TargetType = typeof(ProfilePage) }); //Set icons

            NavigationList.ItemsSource = MenuList;

            //Navigate to Homepage
            Detail = new NavigationPage(new SchedulePage());
            IsPresented = false;
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                if (((MasterPageItem) e.SelectedItem).TargetType == typeof(ProfilePage))
                    (Detail as NavigationPage).PushAsync(new ProfilePage(new Member()));
                else
                    (Detail as NavigationPage).PushAsync((Page)Activator.CreateInstance(((MasterPageItem)e.SelectedItem).TargetType));

                //Creates an instance of the selected page and navigates to it
                IsPresented = false;
                NavigationList.SelectedItem = null;
            }
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}