namespace ForecastingDemand.View
{
    using System.Windows;
    using ForecastingDemand.ViewModel;

    public partial class SimpleMovingAverageView : Window
    {
        public SimpleMovingAverageView(SMAViewModel viewModel)
        {
            this.DataContext = viewModel;
            this.InitializeComponent();
        }
    }
}
