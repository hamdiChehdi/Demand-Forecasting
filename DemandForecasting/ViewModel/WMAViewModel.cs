namespace DemandForecasting.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using DemandForecasting.Model;
    using LiveCharts;

    public class WMAViewModel : ForecastTechniqueViewModel
    {
        private int numberOfPeriods;
        private ObservableCollection<Weight> weights;

        public WMAViewModel(List<Demand> demands, int[] labels)
            : base(demands, labels)
        {
            this.Weights = new ObservableCollection<Weight>();
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
                this.InitializeWeights();
                this.NotifyPropertyChanged(nameof(NumberOfPeriods));
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
            int size = this.ForecastCollection.Count;

            for (int i = 0; i < size; i++)
            {
                this.ForecastCollection[i].Forecasts = 0;
            }

            for (int i = this.NumberOfPeriods; i < size; i++)
            {
                sum = 0;
                int j = i - this.NumberOfPeriods;

                for (int k = 0; k < this.NumberOfPeriods; k++)
                {
                    sum += (this.Weights[this.NumberOfPeriods - k - 1].Alpha * this.ForecastCollection[k + j].Quantity);
                }

                this.ForecastCollection[i].Forecasts = Math.Round(sum / this.NumberOfPeriods, 3);
            }

        }
    }
}

