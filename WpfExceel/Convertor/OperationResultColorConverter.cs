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
                return new SolidColorBrush(Colors.LightGray);
            }

            OperationStatus operationStatus = (OperationStatus)value;

            switch (operationStatus.Status)
            {
                case LoadStatus.Nothing:
                    return new SolidColorBrush(Colors.LightGray);
                case LoadStatus.Loaded:
                    return new SolidColorBrush(Colors.LightGreen);
                case LoadStatus.Ongoing:
                    return new SolidColorBrush(Colors.DeepSkyBlue);
                case LoadStatus.Failed:
                    return new SolidColorBrush(Colors.OrangeRed);
            }
            

            return new SolidColorBrush(Colors.OrangeRed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
