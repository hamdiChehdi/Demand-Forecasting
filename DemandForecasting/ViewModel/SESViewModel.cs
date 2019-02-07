namespace DemandForecasting.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using DemandForecasting.Model;
    using DemandForecasting.MvvmInfrastructure;
    using LiveCharts;
    using LiveCharts.Wpf;

    public class SESViewModel : ForecastTechniqueViewModel
    {
        private double alpha;
        private int startIndex;
        private double yorigin;
        private bool displayOptimization;
        private double errorAvg;

        public SESViewModel(SeriesCollection seriesCollection, List<Demand> demands, int[] labels)
            : base(seriesCollection, demands, labels)
        { 
            this.SeriesCollection.Add(new LineSeries
            {
                Title = "Forecast",
                Values = new ChartValues<double> { },
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 10,
            });
            this.RunAction = new Action<object>(this.Run);
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
            if (this.StartIndex < 1)
            {
                return;
            }

            this.SeriesCollection[1].Values.Clear();
            int size = this.Forecasts.Count;

            for (int i = 0; i < size; i++)
            {
                this.Forecasts[i].Forecasts = 0;
            }

            this.Forecasts[this.StartIndex - 1].Forecasts = this.Forecasts[this.StartIndex - 1].Quantity;
            this.ErrorAvg = 0;
            
            for (int i = 0; i < this.StartIndex; i++)
            {
                this.SeriesCollection[1].Values.Add(0d);
            }

            int k = 0;

            if (this.Yorigin != 0)
            {
                this.Forecasts[this.StartIndex].Forecasts = Math.Round(this.Forecasts[this.StartIndex].Quantity * this.Alpha + (1 - this.Alpha) * this.Yorigin, 3);
                this.SeriesCollection[1].Values.Add(this.Forecasts[this.StartIndex].Forecasts);
                this.ErrorAvg += this.Forecasts[this.StartIndex].Error;
                k++;
            }

            for (int i = this.StartIndex + k; i < size; i++)
            {
                this.Forecasts[i].Forecasts = Math.Round(this.Forecasts[i].Quantity * this.Alpha + (1 - this.Alpha) * this.Forecasts[i - 1].Forecasts, 3);
                this.SeriesCollection[1].Values.Add(this.Forecasts[i].Forecasts);
                this.ErrorAvg += this.Forecasts[i].Error;
            }

            this.ErrorAvg = Math.Round(this.ErrorAvg / (size - this.StartIndex), 3);

            for (int i = this.StartIndex; i < size; i++)
            {
                this.Forecasts[i].Optimization = this.Forecasts[i].Forecasts + this.ErrorAvg;
            }
        }
    }
}
