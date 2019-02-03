namespace ForecastingDemand.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using ForecastingDemand.Model;
    using ForecastingDemand.MvvmInfrastructure;
    using LiveCharts;

    public class SMAViewModel : ViewModelBase
    {
        private int numberOfPeriods;
        private ObservableCollection<Forecast> forecasts;
        private int maxM;

        public SMAViewModel(SeriesCollection seriesCollection, List<Demand> demands)
        {
            this.SeriesCollection = seriesCollection;
            this.InitializeForecasts(demands);
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

        public int MaxM
        {
            get
            {
                return this.maxM;
            }

            set
            {
                this.maxM = value;
                this.NotifyPropertyChanged(nameof(MaxM));
            }
        }

        public SeriesCollection SeriesCollection { get; set; }

        public ObservableCollection<Forecast> Forecasts
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

        public ICommand RunCmd
        {
            get
            {
                return new DelegateCommand(new Action<object>(this.Run));
            }
        }

        private void InitializeForecasts(List<Demand> demands)
        {
            this.Forecasts = new ObservableCollection<Forecast>();

            foreach (Demand demand in demands)
            {
                this.Forecasts.Add(new Forecast(demand));
            }

            this.MaxM = this.Forecasts.Count - 1;
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
