using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Templatez.Api.Http.ObjectResults.ResultValue;

namespace Templatez.Api.Http.ObjectResults
{
    public class ObjectResultUnauthorized : ObjectResult
    {
        public ObjectResultUnauthorized(string value = null) : base(value)
        {
            StatusCode = StatusCodes.Status401Unauthorized;
            Value = !string.IsNullOrEmpty(value) ? new ResultValueMessage(value) : null;
        }
    }
}
