namespace DemandForecasting.View
{
    using System.Windows;
    using DemandForecasting.ViewModel;
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
