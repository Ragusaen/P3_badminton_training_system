using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using application.SystemInterface;
using Xamarin.Forms;

namespace application.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }

        public RequestCreator RequestCreator;

        public event PropertyChangedEventHandler PropertyChanged;


        public BaseViewModel(RequestCreator requestCreator, INavigation navigation)
        {
            RequestCreator = requestCreator;
            Navigation = navigation;
        }

        //Invokes PropertyChanged if not null. Updates UI for property.
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Sets storage to value if they are different.
        //Then calls OnPropertyChanged to update UI
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
