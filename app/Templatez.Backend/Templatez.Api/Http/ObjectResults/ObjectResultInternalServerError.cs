using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Templatez.Api.Http.ObjectResults
{
    public class ObjectResultInternalServerError: ObjectResult
    {
        public ObjectResultInternalServerError(object value = null) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
            Value = value;
        }
    }
}
