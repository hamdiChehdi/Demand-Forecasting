namespace ForecastingDemand.Model
{
    using ForecastingDemand.Convertor;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ForecastingTechniques
    {
        [Description("Simple moving average")]
        SimpleMovingAverage,

        [Description("Weighted moving average")]
        WeightedMovingAverage,

        [Description("Simple exponentiel smoothinh")]
        SimpleExponentielSmoothinh,

        [Description("Holt's procedure")]
        HoltProcedure
    }
}

/*
 * 
 * 1/ Simple moving average = moyenne mobile simple
( in a moving average, the forecast would be calculated as the average of the last few observation
2/ Weighted moving average methods= Moyenne mobile pondérée
(This methods aims at forecasting the future value of sales on the basis of weights given to the most recent observations.)
3/Simple exponentiel smoothinh = lissage exponentiel simple
( A popular way to capture the benefit of the weighted moving average approach while keeping the forecasting procedure simple and easy to use is called exponential smoothing, or occasionally, the exponentially weighted moving average)
4/ Holt's procedure = Lissage de Holt
(Holt’s procedure is a popular technique that is also used to forecast demand data with asimple linear trend. The procedure works by separating the "temporary level", or currentheight of the series, from the trend in the data and developing a smoothed estimate of each component.)

*/