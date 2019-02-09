namespace DemandForecasting.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using DemandForecasting.Model;
    using DemandForecasting.MvvmInfrastructure;
    using LiveCharts;

    public class SMAViewModel : ForecastTechniqueViewModel
    {
        private int numberOfPeriods;

        public SMAViewModel(List<Demand> demands, int[] labels)
            : base(demands, labels)
        {
            this.RunAction = new Action<object>(this.Run);
        }

        public int NumberOfPeriods
        {
            get
            {
                return this.numberOfPeriods;
            }

            set
            {
                this.numberOfPeriods = value;
                this.NotifyPropertyChanged(nameof(NumberOfPeriods));
            }
        }

        private void Run(object input)
        {
            double sum = 0;
            int size = this.ForecastCollection.Count;

            for (int i = 0; i < size; i++)
            {
                this.ForecastCollection[i].Forecasts = 0;
            }

            for (int i = 0; i < this.NumberOfPeriods; i++)
            {
                sum += this.ForecastCollection[i].Quantity;
            }

            for (int i = this.NumberOfPeriods; i < size; i++)
            {
                this.ForecastCollection[i].Forecasts = Math.Round(sum / this.NumberOfPeriods, 3);
                sum -= this.ForecastCollection[i - this.NumberOfPeriods].Quantity;
                sum += this.ForecastCollection[i].Quantity;
            }

        }
    }
}
