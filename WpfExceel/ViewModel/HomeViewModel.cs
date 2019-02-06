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
    using ForecastingDemand.Validation;
    using System.Windows.Controls;
    using System.Threading.Tasks;

    public class HomeViewModel : ViewModelBase
    {
        private string filePath;
        private int[] labels;
        private ForecastingTechniques selectedTechnique;
        private List<Demand> demands;
        private OperationStatus currentStatus;
        private bool isActive;
        private bool enableStart;

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
            this.CurrentStatus = new OperationStatus();
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

        
        public OperationStatus CurrentStatus
        {
            get
            {
                return this.currentStatus;
            }

            set
            {
                this.currentStatus = value;
                this.NotifyPropertyChanged(nameof(CurrentStatus));
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

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }

            set
            {
                this.isActive = value;
                this.NotifyPropertyChanged(nameof(IsActive));
            }
        }

        public bool EnableStart
        {
            get
            {
                return this.enableStart;
            }

            set
            {
                this.enableStart = value;
                this.NotifyPropertyChanged(nameof(EnableStart));
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

        private async void LoadExcel(object input)
        {
            this.demands.Clear();

            ExcelFileNameValidationRule excelValidator = new ExcelFileNameValidationRule();

            ValidationResult validationResult = excelValidator.Validate(this.FilePath, null);

            if (!validationResult.IsValid)
            {
                this.CurrentStatus.ApplyInvalidFileStatus();
                this.NotifyPropertyChanged(nameof(this.CurrentStatus));
                this.EnableStart = false;
                return;
            }

            this.IsActive = true;
            this.CurrentStatus.Status = LoadStatus.Ongoing;
            this.NotifyPropertyChanged(nameof(this.CurrentStatus));
            OperationResult result = await Task.Run(() => ExcelReader.LoadExcel(this.FilePath, this.demands));
            this.CurrentStatus.SetOperationResult(result);
            this.IsActive = false;
            this.NotifyPropertyChanged(nameof(this.CurrentStatus));
            this.EnableStart = false;

            if (this.CurrentStatus.Status == LoadStatus.Loaded)
            {
                this.UpdateChart(demands);
                this.EnableStart = true;
                return;
            }
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
            switch (this.SelectedTechnique)
            {
                case ForecastingTechniques.SimpleMovingAverage:
                    {
                        SMAViewModel sma = new SMAViewModel(this.SeriesCollection, this.demands);
                        SimpleMovingAverageView view = new SimpleMovingAverageView(sma);
                        view.ShowDialog();
                    }
                    break;
                case ForecastingTechniques.WeightedMovingAverage:
                    {
                        WMAViewModel wma = new WMAViewModel(this.SeriesCollection, this.demands);
                        WeightedMovingAverageView view = new WeightedMovingAverageView(wma);
                        view.ShowDialog();
                    }
                    break;
                case ForecastingTechniques.SimpleExponentielSmoothinh:
                    {
                        SESViewModel ses = new SESViewModel(this.SeriesCollection, this.demands, this.Labels);
                        SimpleExponentielSmoothinhView view = new SimpleExponentielSmoothinhView(ses);
                        view.ShowDialog();
                    }
                    break;
            }
            
        }
    }
}
