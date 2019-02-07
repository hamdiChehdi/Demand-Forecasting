namespace DemandForecasting.View
{
    using DemandForecasting.ViewModel;
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
