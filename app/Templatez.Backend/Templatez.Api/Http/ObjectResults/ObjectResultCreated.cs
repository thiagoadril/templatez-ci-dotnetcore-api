using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Templatez.Api.Http.ObjectResults.ResultValue;

namespace Templatez.Api.Http.ObjectResults
{
    public class ObjectResultCreated : ObjectResult
    {
        public ObjectResultCreated(string value = null) : base(value)
        {
            StatusCode = StatusCodes.Status201Created;
            Value = !string.IsNullOrEmpty(value) ? new ResultValueMessage(value) : null;
        }

        public ObjectResultCreated(object value = null) : base(value)
        {
            StatusCode = StatusCodes.Status201Created;
            Value = value;
        }
    }
}
