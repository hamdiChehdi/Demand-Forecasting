namespace DemandForecasting.Model
{
    public class OperationStatus
    {
        public OperationStatus()
        {
            this.Status = LoadStatus.Nothing;
        }

        public LoadStatus Status { get; set; }
        public OperationResult LastOperationResult { get; set; }

        public string GetFailedMessage()
        {
            if (!this.LastOperationResult.IsException())
            {
                return "Load excel operation failed, " + this.LastOperationResult.FailureMessage;
            }

            return "Load excel operation failed, Exception: " + this.LastOperationResult.Exception.Message;
        }

        public void ApplyInvalidFileStatus()
        {
            this.LastOperationResult = OperationResult.FailureResult("Please enter valid excel file");
            this.Status = LoadStatus.Failed;
        }

        public void SetOperationResult(OperationResult result)
        {
            this.LastOperationResult = result;

            if (result.Success)
            {
                this.Status = LoadStatus.Loaded;
            }
            else
            {
                this.Status = LoadStatus.Failed;
            }
        }
    }
}
