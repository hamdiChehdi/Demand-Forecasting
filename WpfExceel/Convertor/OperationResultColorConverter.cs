namespace ForecastingDemand.Convertor 
{
    using System;
    using System.Windows.Data;
    using System.Windows.Media;
    using ForecastingDemand.Model;

    public class OperationResultColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return new SolidColorBrush(Colors.DeepSkyBlue);
            }

            OperationResult result = (OperationResult)value;

            if (result.Success)
            {
                return new SolidColorBrush(Colors.LightGreen);
            }

            return new SolidColorBrush(Colors.OrangeRed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
