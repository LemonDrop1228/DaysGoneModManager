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
    public class UIntToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "1":
                    return new SolidColorBrush(System.Drawing.Color.Goldenrod.ToMediaColor());
                case "0":
                    return new SolidColorBrush(System.Drawing.Color.Black.ToMediaColor());
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
