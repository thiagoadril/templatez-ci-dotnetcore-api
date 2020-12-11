using FluentValidation.Results;
using Templatez.Domain.Core.Commands;
using System.Collections.Generic;

namespace Templatez.Application.Services.Common
{
    public abstract class CommonService : ICommonService
    {
        protected IList<ValidationFailure> ErrorsCommandValidation { get; set; }
        public bool HasInvalidCommand(CommandValidation value)
        {
            if (!value.IsValid)
            {
                ErrorsCommandValidation = value.Errors;
                return true;
            }
            return false;
        }
    }
}
