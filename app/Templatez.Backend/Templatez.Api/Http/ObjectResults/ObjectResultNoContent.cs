using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Templatez.Api.Http.ObjectResults
{
    public class ObjectResultNoContent : ObjectResult
    {
        public ObjectResultNoContent(string value = null) : base(value)
        {
            StatusCode = StatusCodes.Status204NoContent;
            Value = null;
        }
    }
}
