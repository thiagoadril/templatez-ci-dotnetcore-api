using Microsoft.AspNetCore.Mvc;
using Templatez.Api.Http.Errors;
using System;
using System.Net;

namespace Templatez.Api.Controllers.Base
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        [Route("/")]
        [Route("/docs")]
        public IActionResult Index() => new RedirectResult("~/swagger");

        [Route("error/{code}")]
        public IActionResult Error(int code)
            => Enum.IsDefined(typeof(HttpStatusCode), code) ?
            new ObjectResult(new ErrorRoute(code, ((HttpStatusCode)code).ToString())) :
            new ObjectResult(new ErrorRoute((int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString()));
    }
}
