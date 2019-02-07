namespace DemandForecasting.View
{
    using DemandForecasting.ViewModel;
    using MahApps.Metro.Controls;

    public partial class WeightedMovingAverageView : MetroWindow
    {
        public WeightedMovingAverageView(WMAViewModel viewModel)
        {
            this.DataContext = viewModel;
            this.InitializeComponent();
        }
    }
}
