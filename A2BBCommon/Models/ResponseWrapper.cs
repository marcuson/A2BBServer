namespace A2BBCommon.Models
{
    public class ResponseWrapper<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T Payload { get; set; }

        public ResponseWrapper(T payload, string status = "OK", string message = "Success")
        {
            this.Status = status;
            this.Message = message;
            this.Payload = payload;
        }
    }
}