using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using application.Controller;
using Common.Model;
using Xamarin.Forms;

namespace application.UI.Converter
{
    class LeagueEnumStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return StringExtension.SplitCamelCase(Enum.GetName(typeof(TeamMatch.Leagues), (TeamMatch.Leagues) value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
