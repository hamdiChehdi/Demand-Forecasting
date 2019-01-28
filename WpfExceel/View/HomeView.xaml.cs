namespace ForecastingDemand.View
{
    using System.Windows;
    using ForecastingDemand.ViewModel;

    public partial class HomeView : Window
    {
        public HomeView()
        {
            this.InitializeComponent();
            this.DataContext = new HomeViewModel();
        }
    }
}
