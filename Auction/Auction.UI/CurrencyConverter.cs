using System;
using System.Globalization;
using System.Windows.Data;

namespace Auction.UI
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                return $"{doubleValue:0.00} €"; // formatira broj kao string
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue)
            {
                strValue = strValue.Replace(" €", ""); // ukloni " e"
                if (double.TryParse(strValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                {
                    return result; // vraca double
                }
            }
            return 0.0; // vraca default vrednost ako konverzija ne uspe
        }
    }
}
