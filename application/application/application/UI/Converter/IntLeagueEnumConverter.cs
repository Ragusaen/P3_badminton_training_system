using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Common.Model;
using Xamarin.Forms;

namespace application.UI.Converter
{
    class IntLeagueEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is TeamMatch.Leagues ? (int) value : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is int ? Enum.ToObject(targetType, value) : 0;
        }
    }
}
