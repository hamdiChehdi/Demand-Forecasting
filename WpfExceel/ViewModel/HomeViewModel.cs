namespace WpfExceel.ViewModel
{
    using System;
    using System.IO;
    using System.Windows.Input;
    using Microsoft.Win32;
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

        private void ImportExcel(object input)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Excel Worksheets|*.xls";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
            {
                this.FilePath = openFileDialog.FileNames[0];
            }
        }
    }
}
