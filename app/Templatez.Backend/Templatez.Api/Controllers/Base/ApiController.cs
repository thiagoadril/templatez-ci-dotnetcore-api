using Microsoft.AspNetCore.Mvc;
using Templatez.Api.Http.ObjectResults;
using Templatez.Application.Core.Results;
using System.Threading.Tasks;

namespace Templatez.Api.Controllers.Base
{
    public abstract class ApiController : ControllerBase
    {
        protected new async Task<ObjectResult> Response<T>(Task<IResult<T>> result = null) => ResolveResponse(await result);

        protected new ObjectResult Response<T>(IResult<T> result = null) => ResolveResponse(result);

        private ObjectResult ResolveResponse<T>(IResult<T> resultValue)
        {
            switch (resultValue.Status)
            {
                case ResultStatusEnum.Success:
                    return new ObjectResultSuccess(resultValue.Message);
                case ResultStatusEnum.SuccessData:
                    return new ObjectResultSuccess(resultValue.Data);
                case ResultStatusEnum.Created:
                    return new ObjectResultCreated(resultValue.Message);
                case ResultStatusEnum.CreatedData:
                    return new ObjectResultCreated(resultValue.Data);
                case ResultStatusEnum.NoContent:
                    return new ObjectResultNoContent();
                case ResultStatusEnum.Fail:
                    return new ObjectResultFail(resultValue.Message);
                case ResultStatusEnum.FailValidation:
                    return new ObjectResultFailValidation(resultValue.ErrorsValidation);
                case ResultStatusEnum.Unauthorized:
                    return new ObjectResultUnauthorized(resultValue.Message);
                default:
                    return new ObjectResultInternalServerError();
            }
        }
    }
}
