using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Templatez.Api.Http.ObjectResults.ResultValue;

namespace Templatez.Api.Http.ObjectResults
{
    public class ObjectResultSuccess : ObjectResult
    {
        public ObjectResultSuccess(string value = null) : base(value)
        {
            StatusCode = StatusCodes.Status200OK;
            Value = !string.IsNullOrEmpty(value) ? new ResultValueMessage(value) : null;
        }

        public ObjectResultSuccess(object value = null) : base(value)
        {
            StatusCode = StatusCodes.Status200OK;
            Value = value;
        }
    }
}
