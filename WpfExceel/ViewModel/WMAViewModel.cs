namespace ForecastingDemand.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using ForecastingDemand.Model;
    using ForecastingDemand.MvvmInfrastructure;
    using LiveCharts;

    public class WMAViewModel : ViewModelBase
    {
        private int numberOfPeriods;
        private ObservableCollection<Forecast> forecasts;
        private ObservableCollection<Weight> weights;
        private int maxM;

        public WMAViewModel(SeriesCollection seriesCollection, List<Demand> demands)
        {
            this.SeriesCollection = seriesCollection;
            this.InitializeForecasts(demands);
            this.Weights = new ObservableCollection<Weight>();
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
                this.InitializeWeights();
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

        public ObservableCollection<Weight> Weights
        {
            get
            {
                return this.weights;
            }

            set
            {
                this.weights = value;
                this.NotifyPropertyChanged(nameof(Weights));
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

        private void InitializeWeights()
        {
            this.Weights.Clear();

            for (int i = 0; i < this.NumberOfPeriods; i++)
            {
                this.Weights.Add(new Weight(i, 1));
            }
        }

        private void Run(object input)
        {
            double sum = 0;
            int size = this.Forecasts.Count;

            for (int i = this.NumberOfPeriods; i < size; i++)
            {
                sum = 0;
                int j = i - this.NumberOfPeriods;

                for (int k = 0; k < this.NumberOfPeriods; k++)
                {
                    sum += (this.Weights[this.NumberOfPeriods - k - 1].Alpha * this.Forecasts[k + j].Quantity);
                }

                this.Forecasts[i].Forecasts = Math.Round(sum / this.NumberOfPeriods, 3);
            }

        }
    }
}

