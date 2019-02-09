namespace DemandForecasting.View
{
    using DemandForecasting.ViewModel;
    using MahApps.Metro.Controls;

    public partial class HoltView : MetroWindow
    {
        public HoltView(HoltViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
