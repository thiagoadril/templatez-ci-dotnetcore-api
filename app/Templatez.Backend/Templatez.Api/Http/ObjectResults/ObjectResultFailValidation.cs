using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Templatez.Api.Http.Errors;
using System.Collections.Generic;
using System.Linq;

namespace Templatez.Api.Http.ObjectResults
{
    public class ObjectResultFailValidation : ObjectResult
    {
        public ObjectResultFailValidation(IList<ValidationFailure> value) : base(value)
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
            Value = new ErrorRequestValidation(value.GroupBy(a => a.PropertyName, b => b.ErrorMessage)
                .Select(x => new ErrorRequestValidationValue()
                {
                    Field = x.Key.ToLower(),
                    Messages = x.ToList()
                }).ToList());
            }
    }
}