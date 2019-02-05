namespace ForecastingDemand.View
{
    using System.Windows;
    using ForecastingDemand.ViewModel;
    using MahApps.Metro.Controls;

    public partial class HomeView : MetroWindow
    {
        public HomeView()
        {
            this.InitializeComponent();
            this.DataContext = new HomeViewModel();
        }
    }
}
