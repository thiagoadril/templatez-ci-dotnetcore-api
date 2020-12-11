namespace Templatez.Api.Http.Errors
{
    public class ErrorRequestMessage
    {
        public ErrorRequestMessage(string message) => Message = message;
        public string Message { get; set; }
    }
}
