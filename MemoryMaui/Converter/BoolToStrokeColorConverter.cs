using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMaui.Converter
{
    public class BoolToStrokeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue && boolValue)
            {
                return new SolidColorBrush(Colors.YellowGreen);
            }
            else
            {
                // Standardwert (z.B. Schwarz) zurückgeben, wenn der bool-Wert false ist
                return new SolidColorBrush(Colors.LightGray);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
