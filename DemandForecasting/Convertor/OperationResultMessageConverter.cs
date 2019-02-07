namespace DemandForecasting.Convertor
{    
    using System;
    using System.Windows.Data;
    using DemandForecasting.Model;

    public class OperationResultMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return "Ready ...";
            }

            OperationStatus operationStatus = (OperationStatus)value;

            switch (operationStatus.Status)
            {
                case LoadStatus.Nothing:
                    return "Ready ...";
                case LoadStatus.Loaded:
                    return "The selected file succefully loaded";
                case LoadStatus.Ongoing:
                    return "Loading file in progress ...";
                case LoadStatus.Failed:
                    return operationStatus.GetFailedMessage();
            }

            return "Ready ...";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
