namespace Com.Garment.Shipping.ETL.Service.ViewModels
{
    public class ResponseSuccessViewModel
    {
        public ResponseSuccessViewModel(string message, object data = null, object info = null)
        {
            this.message = message;
            this.data = data;
            this.info = info;
        }
        public string message {get; set;}
        public object? data {get; set;}
        public object? info {get; set;}
    }
}