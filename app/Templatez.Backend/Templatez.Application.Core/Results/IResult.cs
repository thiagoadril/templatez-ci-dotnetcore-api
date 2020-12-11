using FluentValidation.Results;
using System.Collections.Generic;

namespace Templatez.Application.Core.Results
{
    public interface IResult
    {
        ResultStatusEnum Status { get; set; }
        string Message { get; set; }
        public IList<ValidationFailure> ErrorsValidation { get; set; }
    }

    public interface IResult<out T> : IResult
    {
        T Data { get; }
    }
}
