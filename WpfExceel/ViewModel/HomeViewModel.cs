namespace ForecastingDemand.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;  
    using ForecastingDemand.Excel;
    using ForecastingDemand.Model;
    using ForecastingDemand.MvvmInfrastructure;
    using Microsoft.Win32;
    using LiveCharts;
    using LiveCharts.Wpf;

    public class HomeViewModel : ViewModelBase
    {
        private string filePath;
        private int[] labels;

        public HomeViewModel()
        {
            this.SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Demand",
                    Values = new ChartValues<double> {},
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 10,
                }
            };
        }

        public string FilePath
        {
            get
            {
                return this.filePath;
            }

            set
            {
                this.filePath = value;
                this.NotifyPropertyChanged("FilePath");
            }
        }

        public SeriesCollection SeriesCollection { get; set; }

        public int[] Labels
        {
            get
            {
                return this.labels;
            }

            set
            {
                this.labels = value;
                this.NotifyPropertyChanged("Labels");
            }
        }

        public ICommand ImportExcelCmd
        {
            get
            {
                return new DelegateCommand(new Action<object>(this.ImportExcel));
            }
        }

        public ICommand StartCmd
        {
            get
            {
                return new DelegateCommand(new Action<object>(this.LoadExcel));
            }
        }

        private void ImportExcel(object input)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Excel Worksheets|*.xls|Excel Worksheets|*.xlsx";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
            {
                this.FilePath = openFileDialog.FileNames[0];
            }
        }

        private void LoadExcel(object input)
        {
            List<Demand> demands = new List<Demand>();

            OperationResult result = ExcelReader.LoadExcel(this.FilePath, demands);

            if (result.Success)
            {
                MessageBox.Show("WPF App", "Succefully loaded");
                this.UpdateChart(demands);
                return;
            }

            if (!result.IsException())
            {
                MessageBox.Show(result.FailureMessage, "Load excel operation failed ", MessageBoxButton.OK);
                return;
            }

            MessageBox.Show(result.Exception.Message, "Load excel operation failed ", MessageBoxButton.OK);
        }

        private void UpdateChart(List<Demand> demands)
        {
            int size = demands.Count;
            this.SeriesCollection[0].Values.Clear();
            this.Labels = new int[size];

            for (int idemand = 0; idemand < size; idemand++)
            {
                this.Labels[idemand] = demands[idemand].period;
                this.SeriesCollection[0].Values.Add(demands[idemand].quantity);
            }
        }
    }
}
