namespace DemandForecasting.ViewModel
{
    using System;
    using System.Collections.Generic;
    using DemandForecasting.Model;
    using LiveCharts;
    using LiveCharts.Wpf;

    public class SESViewModel : ForecastTechniqueViewModel
    {
        private double alpha;
        private int startIndex;
        private double yorigin;
        private double errorAvg;

        public SESViewModel(List<Demand> demands, int[] labels)
            : base(demands, labels)
        { 
            this.RunAction = new Action<object>(this.Run);
            this.Optimizations = new ChartValues<double>();
            this.DisplayForecastChart = true;
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

        public int StartIndex
        {
            get
            {
                return this.startIndex;
            }

            set
            {
                this.startIndex = value;
                this.NotifyPropertyChanged(nameof(StartIndex));
            }
        }

        public double Yorigin
        {
            get
            {
                return this.yorigin;
            }

            set
            {
                this.yorigin = value;
                this.NotifyPropertyChanged(nameof(Yorigin));
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
            if (this.StartIndex < 1)
            {
                return;
            }

            // Update chart
            this.Forecasts.Clear();
            this.Optimizations.Clear();

            int size = this.ForecastCollection.Count;

            for (int i = 0; i < size; i++)
            {
                this.ForecastCollection[i].Forecasts = 0;
            }

            this.ForecastCollection[this.StartIndex - 1].Forecasts = this.ForecastCollection[this.StartIndex - 1].Quantity;
            this.ErrorAvg = 0;
            
            for (int i = 0; i < this.StartIndex; i++)
            {
                // Update chart
                this.Forecasts.Add(0d);
                this.Optimizations.Add(0d);
            }

            int k = 0;

            if (this.Yorigin != 0)
            {
                this.ForecastCollection[this.StartIndex].Forecasts = Math.Round(this.ForecastCollection[this.StartIndex].Quantity * this.Alpha + (1 - this.Alpha) * this.Yorigin, 3);
                this.Forecasts.Add(this.ForecastCollection[this.StartIndex].Forecasts);
                this.ErrorAvg += this.ForecastCollection[this.StartIndex].Error;
                k++;
            }

            for (int i = this.StartIndex + k; i < size; i++)
            {
                this.ForecastCollection[i].Forecasts = Math.Round(this.ForecastCollection[i].Quantity * this.Alpha + (1 - this.Alpha) * this.ForecastCollection[i - 1].Forecasts, 3);
                this.Forecasts.Add(this.ForecastCollection[i].Forecasts);
                this.ErrorAvg += this.ForecastCollection[i].Error;
            }

            this.ErrorAvg = Math.Round(this.ErrorAvg / (size - this.StartIndex), 3);

            for (int i = this.StartIndex; i < size; i++)
            {
                this.ForecastCollection[i].Optimization = this.ForecastCollection[i].Forecasts + this.ErrorAvg;
                this.Optimizations.Add(this.ForecastCollection[i].Optimization);
            }
        }
    }
}
