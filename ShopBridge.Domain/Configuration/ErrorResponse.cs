namespace ShopBridge.Domain.Configuration
{
    public class ErrorResponse
    {
        public ErrorResponse(int StatusCode, string message)
        {
            statusCode = StatusCode;
            messages = (message.Split(","));
        }
        public int statusCode { get; set; }
        public string[] messages { get; set; }
    }
}
