namespace WpfExceel.View
{
    using System.Windows;
    using WpfExceel.ViewModel;

    public partial class HomeView : Window
    {
        public HomeView()
        {
            this.InitializeComponent();
            this.DataContext = new HomeViewModel();
        }
    }
}
