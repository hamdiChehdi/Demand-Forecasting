namespace ForecastingDemand.Convertor
{    
    using System;
    using System.Windows.Data;
    using ForecastingDemand.Model;

    public class OperationResultMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return "Ready ...";
            }

            OperationResult result = (OperationResult)value;

            if (result.Success)
            {
                return "The selected file succefully loaded";
            }

            if (!result.IsException())
            {
                return "Load excel operation failed, " + result.FailureMessage;
            }

            return "Load excel operation failed, Exception: " + result.Exception.Message;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
