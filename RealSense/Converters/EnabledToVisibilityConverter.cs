using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RealSense.Converters
{
    class EnabledToVisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof (Visibility)) return false;
            switch (bool.Parse(value.ToString()))
            {
                case true:
                    return Visibility.Visible;
                case false:
                    return Visibility.Hidden;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility val;
            Enum.TryParse(value.ToString(), out val);
            switch (val)
            {
                case Visibility.Visible:
                    return true;
                case Visibility.Collapsed:
                    return false;
                case Visibility.Hidden:
                    return false;
            }
            return false;
        }
    }
}
