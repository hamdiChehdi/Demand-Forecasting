namespace WpfExceel.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using System.Windows.Input;
    using Microsoft.Win32;
    using WpfExceel.Excel;
    using WpfExceel.Model;
    using WpfExceel.MvvmInfrastructure;

    public class HomeViewModel : ViewModelBase
    {
        private string filePath;

        public HomeViewModel()
        {

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
                return;
            }

            if (!result.IsException())
            {
                MessageBox.Show(result.FailureMessage, "Load excel operation failed ", MessageBoxButton.OK);
                return;
            }

            MessageBox.Show(result.Exception.Message, "Load excel operation failed ", MessageBoxButton.OK);
        }
    }
}
