namespace ForecastingDemand.View
{
    using ForecastingDemand.ViewModel;
    using MahApps.Metro.Controls;

    public partial class SimpleExponentielSmoothinhView : MetroWindow
    {
        public SimpleExponentielSmoothinhView(SESViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
