using System;
using System.Windows.Data;

namespace X4SaveEditor
{
    public class DateValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime dtDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var tempValue = 0.0;
            double.TryParse((string)value, out tempValue);
            dtDate = dtDate.AddSeconds(tempValue);
            return dtDate.ToString(culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime valueDate = DateTime.Parse((string)value, culture);
            return (int)(valueDate.Ticks - new DateTime(1970, 1, 1, 0, 0, 0, 0).Ticks) / (double)10000000;
        }
    }

}