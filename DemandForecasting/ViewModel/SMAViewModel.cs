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

        public SMAViewModel(SeriesCollection seriesCollection, List<Demand> demands, int[] labels)
            : base(seriesCollection, demands, labels)
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

            for (int i = 0; i < this.NumberOfPeriods; i++)
            {
                sum += this.Forecasts[i].Quantity;
            }

            int size = this.Forecasts.Count;

            for (int i = this.NumberOfPeriods; i < size; i++)
            {
                this.Forecasts[i].Forecasts = Math.Round(sum / this.NumberOfPeriods, 3);
                sum -= this.Forecasts[i - this.NumberOfPeriods].Quantity;
                sum += this.Forecasts[i].Quantity;
            }

        }
    }
}
