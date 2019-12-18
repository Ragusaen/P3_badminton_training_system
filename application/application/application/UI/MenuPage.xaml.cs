using application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.SystemInterface;
using Common.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage
    {
        public List<MasterPageItem> MenuList { get; set; }

        public MenuPage(RequestCreator requestCreator) : base(requestCreator)
        {
            InitializeComponent();

            //Sets BindingContext ViewModel
            MenuViewModel vm = new MenuViewModel(RequestCreator, Navigation);
            BindingContext = vm;
            vm.Navigation = Navigation;

            //Putting items in the BurgerMenu
            MenuList = new List<MasterPageItem>();

            MenuList.Add(new MasterPageItem() { Title = "Schedule", TargetType = typeof(SchedulePage) }); //Set icons
            MenuList.Add(new MasterPageItem() { Title = "Your Profile", TargetType = typeof(ProfilePage) }); //Set icons
            if((RequestCreator.LoggedInMember.MemberType & MemberType.Trainer) == MemberType.Trainer)
            MenuList.Add(new MasterPageItem() { Title = "Administrator", TargetType = typeof(AdministratorPage) }); //Set icons

            NavigationList.ItemsSource = MenuList;

            //Navigate to Homepage
            Detail = new NavigationPage(new SchedulePage(RequestCreator));
            IsPresented = false;
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null) //Creates an instance of the selected page and navigates to it
            {
                if (((MasterPageItem) e.SelectedItem).TargetType == typeof(ProfilePage))
                    (Detail as NavigationPage).PushAsync(new ProfilePage(RequestCreator.LoggedInMember.Id, RequestCreator));
                else
                    (Detail as NavigationPage).PushAsync((Page)Activator.CreateInstance((((MasterPageItem)e.SelectedItem).TargetType), RequestCreator));

                IsPresented = false;
                NavigationList.SelectedItem = null;
            }
        }

        //Logs out of app 
        private void Logout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage(RequestCreator));
        }
    }
}