namespace ForecastingDemand.Model
{
    using ForecastingDemand.MvvmInfrastructure;

    public class Weight : ViewModelBase
    {
        private int index;
        private double alpha;

        public Weight(int index, double alpha)
        {
            this.Alpha = alpha;
            this.Index = index;
        }

        public int Index
        {
            get
            {
                return this.index;
            }

            set
            {
                this.index = value;
                this.NotifyPropertyChanged(nameof(Index));
            }
        }
    
        public double Alpha
        {
            get
            {
                return this.alpha;
            }

            set
            {
                this.alpha = value;
                this.NotifyPropertyChanged(nameof(Alpha));
            }
        }

    }
}
