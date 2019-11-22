using Common.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace application.UI.Converter
{
    class LineupPositionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pos = ((Lineup.PositionType, int))value;
            string res = (pos.Item2 + 1) + ". ";

            switch (pos.Item1)
            {
                case Lineup.PositionType.MensDouble:
                    res += "HD";
                    break;
                case Lineup.PositionType.MensSingle:
                    res += "HS";
                    break;
                case Lineup.PositionType.WomensDouble:
                    res += "DD";
                    break;
                case Lineup.PositionType.WomensSingle:
                    res += "DS";
                    break;
                case Lineup.PositionType.MixDouble:
                    res += "MD";
                    break;
            }
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
