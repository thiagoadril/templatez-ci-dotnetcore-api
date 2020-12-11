namespace Templatez.Api.Http.ObjectResults.ResultValue
{
    public class ResultValueMessage
    {
        public ResultValueMessage(string message) => Message = message;
        public string Message { get; set; }
    }
}
