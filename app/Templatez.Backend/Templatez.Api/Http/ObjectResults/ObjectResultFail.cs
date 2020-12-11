using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Templatez.Api.Http.ObjectResults.ResultValue;

namespace Templatez.Api.Http.ObjectResults
{
    public class ObjectResultFail : ObjectResult
    {
        public ObjectResultFail(string value = null) : base(value)
        {
            StatusCode = StatusCodes.Status400BadRequest;
            Value = !string.IsNullOrEmpty(value) ? new ResultValueMessage(value) : null;
        }
    }
}
