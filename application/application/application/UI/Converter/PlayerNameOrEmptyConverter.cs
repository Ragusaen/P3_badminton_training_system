using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Common.Model;
using Xamarin.Forms;

namespace application.UI.Converter
{
    class PlayerNameOrEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? "No Player" : ((Player)value).Member.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
