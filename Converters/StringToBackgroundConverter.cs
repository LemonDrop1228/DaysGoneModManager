using DaysGoneModManager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Drawing;

namespace DaysGoneModManager.Converters
{
    public class StringToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((value as string).NullOrEmpty())
            {
                case false:
                    return new SolidColorBrush(System.Drawing.Color.Green.ToMediaColor());
                case true:
                    return new SolidColorBrush(System.Drawing.Color.Red.ToMediaColor());
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
