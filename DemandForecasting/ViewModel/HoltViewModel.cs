namespace DemandForecasting.ViewModel
{
    using System;
    using System.Collections.Generic;
    using DemandForecasting.Model;
    using LiveCharts;
    using LiveCharts.Wpf;

    public class HoltViewModel : ForecastTechniqueViewModel
    {
        private double alpha;
        private double beta;
        private bool displayOptimization;
        private double errorAvg;

        public HoltViewModel(List<Demand> demands, int[] labels)
            : base(demands, labels)
        {
            this.RunAction = new Action<object>(this.Run);
            this.Optimizations = new ChartValues<double>();
        }

        public double Alpha
        {
            get
            {
                return this.alpha;
            }

            set
            {
                this.alpha = value;
                this.NotifyPropertyChanged(nameof(Alpha));
            }
        }

        public double Beta
        {
            get
            {
                return this.beta;
            }

            set
            {
                this.beta = value;
                this.NotifyPropertyChanged(nameof(Beta));
            }
        }

        public ChartValues<double> Optimizations { get; set; }

        public bool DisplayOptimization
        {
            get
            {
                return this.displayOptimization;
            }

            set
            {
                this.displayOptimization = value;
                this.NotifyPropertyChanged(nameof(DisplayOptimization));
            }
        }

        public double ErrorAvg
        {
            get
            {
                return this.errorAvg;
            }

            set
            {
                this.errorAvg = value;
                this.NotifyPropertyChanged(nameof(ErrorAvg));
            }
        }

        private void Run(object input)
        {
            // Update chart
            this.Forecasts.Clear();
            this.Optimizations.Clear();

            int size = this.ForecastCollection.Count;

            for (int i = 0; i < size; i++)
            {
                this.ForecastCollection[i].Forecasts = 0;
            }

            this.ForecastCollection[0].Forecasts = 0;
            this.ErrorAvg = 0;

            // Update chart
            this.Forecasts.Add(0d);
            this.Optimizations.Add(0d);

            double a = this.ForecastCollection[0].Quantity;
            double b = 0;

            for (int i = 1; i < size; i++)
            {
                this.ForecastCollection[i].Forecasts = Math.Round(a + b, 3);
                double pa = a;
                a = this.Alpha * this.ForecastCollection[i].Quantity + (1 - this.Alpha) * (a + b);
                b = this.Beta * (a - pa) + (1 - this.Beta) * b;        
                this.ErrorAvg += this.ForecastCollection[i].Error;
                
                // Update chart
                this.Forecasts.Add(this.ForecastCollection[i].Forecasts);
            }

            this.ErrorAvg = Math.Round(this.ErrorAvg / (size - 1), 3);

            for (int i = 1; i < size; i++)
            {
                this.ForecastCollection[i].Optimization = this.ForecastCollection[i].Forecasts + this.ErrorAvg;

                // Update chart
                this.Optimizations.Add(this.ForecastCollection[i].Optimization);
            }
        }
    }
}
