using System;
using FluentValidation;
using Templatez.Domain.Core.Commands;

namespace Templatez.Domain.Validations.Commons.Ids
{
    public class IdCommandValidation : AbstractValidator<Command>
    {
        public IdCommandValidation()
        {
            RuleFor(c => c.Id).NotEqual(Guid.Empty);
        }
    }
}
