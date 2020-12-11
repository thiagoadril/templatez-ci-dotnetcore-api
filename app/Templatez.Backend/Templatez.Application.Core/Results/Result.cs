using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Templatez.Application.Core.Results
{
    public class Result : IResult
    {
        protected Result() { }

        public ResultStatusEnum Status { get; set; }

        public string Message { get; set; }

        public IList<ValidationFailure> ErrorsValidation { get; set; }

        //200 ----------------------------
        public static IResult Success() => new Result { Status = ResultStatusEnum.Success };

        public static IResult Success(string message) => new Result { Status = ResultStatusEnum.Success, Message = message };

        public static Task<IResult> SuccessAsync() => Task.FromResult(Success());

        public static Task<IResult> SuccessAsync(string message) => Task.FromResult(Success(message));

        //201 ----------------------------
        public static IResult Created() => new Result { Status = ResultStatusEnum.Created };

        public static IResult Created(string message) => new Result { Status = ResultStatusEnum.Created, Message = message };

        public static Task<IResult> CreatedAsync() => Task.FromResult(Created());

        public static Task<IResult> CreatedAsync(string message) => Task.FromResult(Created(message));

        //204 ----------------------------
        public static IResult NoContent() => new Result { Status = ResultStatusEnum.Created };

        public static Task<IResult> NoContentAsync() => Task.FromResult(NoContent());

        //400 ----------------------------
        public static IResult Fail() => new Result { Status = ResultStatusEnum.Fail };

        public static IResult Fail(string message) => new Result { Status = ResultStatusEnum.Fail, Message = message };
        public static Task<IResult> FailAsync() => Task.FromResult(Fail());

        public static Task<IResult> FailAsync(string message) => Task.FromResult(Fail(message));

        //401 ----------------------------
        public static IResult Unauthorized() => new Result { Status = ResultStatusEnum.Unauthorized };
        public static IResult Unauthorized(string message) => new Result { Status = ResultStatusEnum.Unauthorized, Message = message };
        public static Task<IResult> UnauthorizedAsync() => Task.FromResult(Unauthorized());

        public static Task<IResult> UnauthorizedAsync(string message) => Task.FromResult(Unauthorized(message));


        //422 ----------------------------
        public static IResult FailValidation(IList<ValidationFailure> validationFailures)
            => new Result { Status = ResultStatusEnum.FailValidation, ErrorsValidation = validationFailures };

        public static Task<IResult> FailValidationAsync(IList<ValidationFailure> validationFailures)
            => Task.FromResult(FailValidation(validationFailures));
    }

    public class Result<T> : Result, IResult<T>
    {
        protected Result() { }

        public T Data { get; set; }

        //200 ----------------------------
        public static new IResult<T> Success() => new Result<T> { Status = ResultStatusEnum.Success };

        public static new IResult<T> Success(string message) => new Result<T> { Status = ResultStatusEnum.Success, Message = message };

        public static IResult<T> Success(T data) => new Result<T> { Status = ResultStatusEnum.SuccessData, Data = data };

        public static new Task<IResult<T>> SuccessAsync() => Task.FromResult(Success());

        public static new Task<IResult<T>> SuccessAsync(string message) => Task.FromResult(Success(message));

        public static Task<IResult<T>> SuccessAsync(T data) => Task.FromResult(Success(data));

        //201 ----------------------------
        public static new IResult<T> Created() => new Result<T> { Status = ResultStatusEnum.Created };

        public static new IResult<T> Created(string message) => new Result<T> { Status = ResultStatusEnum.Created, Message = message };

        public static IResult<T> Created(T data) => new Result<T> { Status = ResultStatusEnum.CreatedData, Data = data };

        public static new Task<IResult<T>> CreatedAsync() => Task.FromResult(Created());

        public static new Task<IResult<T>> CreatedAsync(string message) => Task.FromResult(Created(message));

        public static Task<IResult<T>> CreatedAsync(T data) => Task.FromResult(Created(data));

        //400 ----------------------------

        public static new IResult<T> Fail() => new Result<T> { Status = ResultStatusEnum.Fail };

        public static new IResult<T> Fail(string message) => new Result<T> { Status = ResultStatusEnum.Fail, Message = message };

        public static new Task<IResult<T>> FailAsync() => Task.FromResult(Fail());

        public static new Task<IResult<T>> FailAsync(string message) => Task.FromResult(Fail(message));

        //401 ----------------------------

        public static new IResult<T> Unauthorized() => new Result<T> { Status = ResultStatusEnum.Unauthorized };

        public static new IResult<T> Unauthorized(string message) => new Result<T> { Status = ResultStatusEnum.Unauthorized, Message = message };

        public static new Task<IResult<T>> UnauthorizedAsync() => Task.FromResult(Unauthorized());

        public static new Task<IResult<T>> UnauthorizedAsync(string message) => Task.FromResult(Unauthorized(message));

        //422 ----------------------------
        public static new IResult<T> FailValidation(IList<ValidationFailure> validationFailures)
            => new Result<T> { Status = ResultStatusEnum.FailValidation, ErrorsValidation = validationFailures };

        public static new Task<IResult<T>> FailValidationAsync(IList<ValidationFailure> validationFailures) => Task.FromResult(FailValidation(validationFailures));
    }
}
