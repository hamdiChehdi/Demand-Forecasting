
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DemandForecasting.View
{
    using DemandForecasting.ViewModel;
    using MahApps.Metro.Controls;

    public partial class ForecastChartsView : MetroWindow
    {
        public ForecastChartsView(ForecastTechniqueViewModel viewModel)
        {
            this.DataContext = viewModel;
            this.InitializeComponent();
        }
    }
}
