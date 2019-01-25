namespace WpfExceel
{
    using System.Windows;
    using WpfExceel.Excel;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            ExcelReader.ReadExcel();
        }
    }
}
