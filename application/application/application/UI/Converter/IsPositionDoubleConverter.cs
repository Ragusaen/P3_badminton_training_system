using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Common.Model;
using Xamarin.Forms;

namespace application.UI.Converter
{
    class IsPositionDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var position = (Tuple<Lineup.PositionType, int>)value;
            return Lineup.PositionType.Double.HasFlag(position.Item1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
