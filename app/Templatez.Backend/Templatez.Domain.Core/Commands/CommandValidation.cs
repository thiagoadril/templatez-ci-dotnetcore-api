using System.Collections.Generic;
using FluentValidation.Results;

namespace Templatez.Domain.Core.Commands
{
    public class CommandValidation
    {
        //public CommandValidation(bool isValid, IList<ValidationFailure> errors)
        //{
        //    IsValid = isValid;
        //    Errors = errors;
        //}

        public CommandValidation(ValidationResult result)
        {
            IsValid = result.IsValid;
            Errors = result.Errors;
        }


        public virtual bool IsValid { get; }
        public IList<ValidationFailure> Errors { get; }
    }
}
