namespace ForecastingDemand.Model
{
    public enum LoadStatus
    {
        Loaded, // Valid file already loaded
        Ongoing, // Load the current file in progress
        Failed, // Load Failed
        Nothing // nothing loaded
    }
}
