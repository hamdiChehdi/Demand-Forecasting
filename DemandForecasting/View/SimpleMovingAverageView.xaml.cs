namespace DemandForecasting.View
{
    using DemandForecasting.ViewModel;
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
