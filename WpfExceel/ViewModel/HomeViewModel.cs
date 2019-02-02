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
    using ForecastingDemand.View;

    public class HomeViewModel : ViewModelBase
    {
        private string filePath;
        private int[] labels;
        private ForecastingTechniques selectedTechnique;
        private List<Demand> demands; 

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

            this.demands = new List<Demand>();
        }

        public ForecastingTechniques SelectedTechnique
        {
            get
            {
                return this.selectedTechnique;
            }

            set
            {
                this.selectedTechnique = value;
                this.NotifyPropertyChanged(nameof(SelectedTechnique));
            }
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
                this.NotifyPropertyChanged(nameof(FilePath));
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
                this.NotifyPropertyChanged(nameof(Labels));
            }
        }

        public ICommand ImportExcelCmd
        {
            get
            {
                return new DelegateCommand(new Action<object>(this.ImportExcel));
            }
        }

        public ICommand LoadExcelCmd
        {
            get
            {
                return new DelegateCommand(new Action<object>(this.LoadExcel));
            }
        }

        public ICommand StartCmd
        {
            get
            {
                return new DelegateCommand(new Action<object>(this.Start));
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
            this.demands.Clear();

            OperationResult result = ExcelReader.LoadExcel(this.FilePath, this.demands);

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

        private void Start(object input)
        {
            SMAViewModel sma = new SMAViewModel(this.SeriesCollection, this.demands);
            SimpleMovingAverageView view = new SimpleMovingAverageView(sma);
            view.ShowDialog();
        }
    }
}
