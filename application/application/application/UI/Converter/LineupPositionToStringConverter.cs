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
                    res += "MD";
                    break;
                case Lineup.PositionType.MensSingle:
                    res += "MS";
                    break;
                case Lineup.PositionType.WomensDouble:
                    res += "WD";
                    break;
                case Lineup.PositionType.WomensSingle:
                    res += "WS";
                    break;
                case Lineup.PositionType.MixDouble:
                    res += "XD";
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
