namespace ForecastingDemand.Model
{
    using ForecastingDemand.MvvmInfrastructure;

    public class Forecast : ViewModelBase
    {
        private int period;
        private double quantity;
        private double forecasts;

        public Forecast(Demand demand)
        {
            this.period = demand.period;
            this.quantity = demand.quantity;
        }

        public int Period
        {
            get
            {
                return this.period;
            }

            set
            {
                this.period = value;
                this.NotifyPropertyChanged(nameof(Period));
            }
        }

        public double Quantity
        {
            get
            {
                return this.quantity;
            }

            set
            {
                this.quantity = value;
                this.NotifyPropertyChanged(nameof(Quantity));
            }
        }

        public double Forecasts
        {
            get
            {
                return this.forecasts;
            }

            set
            {
                this.forecasts = value;
                this.NotifyPropertyChanged(nameof(Forecasts));
            }
        }
    }
}
