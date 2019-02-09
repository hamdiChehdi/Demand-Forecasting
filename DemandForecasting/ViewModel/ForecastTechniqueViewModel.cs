namespace DemandForecasting.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using DemandForecasting.Model;
    using DemandForecasting.MvvmInfrastructure;
    using LiveCharts;

    public class ForecastTechniqueViewModel :ViewModelBase
    {
        private ObservableCollection<Forecast> forecastCollection;
        private int maxM;
        private int[] labels;

        public ForecastTechniqueViewModel(List<Demand> demands, int[] labels)
        {
            this.Labels = labels;
            this.Forecasts = new ChartValues<double>();
            this.InitializeForecasts(demands);
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

        public ChartValues<double> Demands { get; set; }
        public ChartValues<double> Forecasts { get; set; }

        public Action<object> RunAction { get; set; }

        public ObservableCollection<Forecast> ForecastCollection
        {
            get
            {
                return this.forecastCollection;
            }

            set
            {
                this.forecastCollection = value;
                this.NotifyPropertyChanged(nameof(ForecastCollection));
            }
        }

        public int[] Labels
        {
            get
            {
                return this.labels;
            }

            set
            {
                this.labels = value;
                this.NotifyPropertyChanged(nameof(Labels));
            }
        }

        public ICommand RunCmd
        {
            get
            {
                return new DelegateCommand(this.RunAction);
            }
        }

        private void InitializeForecasts(List<Demand> demands)
        {
            this.ForecastCollection = new ObservableCollection<Forecast>();
            this.Demands = new ChartValues<double>();

            foreach (Demand demand in demands)
            {
                this.ForecastCollection.Add(new Forecast(demand));
                this.Demands.Add(demand.quantity);
            }

            this.MaxM = this.ForecastCollection.Count - 1;
        }
    }
}
