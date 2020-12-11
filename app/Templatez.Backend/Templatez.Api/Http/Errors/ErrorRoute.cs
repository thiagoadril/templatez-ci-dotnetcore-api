using Newtonsoft.Json;

namespace Templatez.Api.Http.Errors
{
    public class ErrorRoute
    {
        public int StatusCode { get; private set; }

        public string StatusMessage { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }

        public ErrorRoute(int statusCode, string statusMessage)
        {
            StatusCode = statusCode;
            StatusMessage = statusMessage;
        }

        public ErrorRoute(int statusCode, string statusMessage, string message)
            : this(statusCode, statusMessage)
        {
            Message = message;
        }
    }
}
