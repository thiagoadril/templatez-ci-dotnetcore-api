using System.Collections.Generic;

namespace Templatez.Api.Http.Errors
{
    public class ErrorRequestValidation
    {
        public ErrorRequestValidation(List<ErrorRequestValidationValue> errors)
        {
            Errors = errors;
        }
        public List<ErrorRequestValidationValue> Errors { get; set; }
    }
}
