using System;
using FluentValidation;
using Templatez.Domain.Commands.Customers;
using Templatez.Domain.Validations.Customers.Messages;

namespace Templatez.Domain.Validations.Customers
{
    public abstract class CustomerValidation<T> : AbstractValidator<T> where T : CustomerCommand
    {
        protected void ValidateId() => RuleFor(c => c.Id)
            .NotEqual(Guid.Empty)
            .WithMessage(CustomerValidationMessages.IdInvalid);

        protected void ValidateNameCreate() => RuleFor(c => c.Name)
            .NotEmpty().WithMessage(CustomerValidationMessages.NameRequired)
            .MinimumLength(3).WithMessage(CustomerValidationMessages.NameMinimumLength)
            .MaximumLength(100).WithMessage(CustomerValidationMessages.NameMaximumLength);

        protected void ValidateEmailCreate() => RuleFor(c => c.Email)
            .NotEmpty().WithMessage(CustomerValidationMessages.EmailRequired)
            .EmailAddress().WithMessage(CustomerValidationMessages.EmailInvalid);

        protected void ValidateNameUpdate() => RuleFor(c => c.Name)
            .MinimumLength(3).WithMessage(CustomerValidationMessages.NameMinimumLength)
            .MaximumLength(100).WithMessage(CustomerValidationMessages.NameMaximumLength);

        protected void ValidateEmailUpdate() => RuleFor(c => c.Email)
            .EmailAddress().WithMessage(CustomerValidationMessages.EmailInvalid);
    }
}
