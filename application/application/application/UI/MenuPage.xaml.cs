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
        public Member Member { get; set; }

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
            Detail = new NavigationPage(new SchedulePage(Member));
            IsPresented = false;
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Creates an instance of the selected page and navigates to it
            (Detail as NavigationPage).PushAsync((Page)Activator.CreateInstance(((MasterPageItem)e.SelectedItem).TargetType, Member));
            IsPresented = false;
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}