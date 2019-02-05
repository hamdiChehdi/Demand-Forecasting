namespace ForecastingDemand.View
{
    using System.Windows;
    using ForecastingDemand.ViewModel;
    using MahApps.Metro.Controls;

    public partial class SimpleMovingAverageView : MetroWindow
    {
        public SimpleMovingAverageView(SMAViewModel viewModel)
        {
            this.DataContext = viewModel;
            this.InitializeComponent();
        }
    }
}
