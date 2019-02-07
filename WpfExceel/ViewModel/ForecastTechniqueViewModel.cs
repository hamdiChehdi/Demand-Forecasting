namespace ForecastingDemand.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using ForecastingDemand.Model;
    using ForecastingDemand.MvvmInfrastructure;
    using LiveCharts;

    public class ForecastTechniqueViewModel :ViewModelBase
    {
        private ObservableCollection<Forecast> forecasts;
        private int maxM;
        private int[] labels;

        public ForecastTechniqueViewModel(SeriesCollection seriesCollection, List<Demand> demands, int[] labels)
        {
            this.SeriesCollection = seriesCollection;
            this.Labels = labels;
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

        public Action<object> RunAction { get; set; }

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
            this.Forecasts = new ObservableCollection<Forecast>();

            foreach (Demand demand in demands)
            {
                this.Forecasts.Add(new Forecast(demand));
            }

            this.MaxM = this.Forecasts.Count - 1;
        }
    }
}
