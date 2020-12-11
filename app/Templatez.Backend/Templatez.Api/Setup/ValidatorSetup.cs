using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Templatez.Domain.Commands.Customers;
using Templatez.Domain.Validations.Customers;
using Templatez.Infra.CrossCutting.Extensions;

namespace Templatez.Api.Setup
{
    public static class ValidatorSetup
    {
        public static void AddValidatorSetup(this IServiceCollection services)
        {
            // Throw if object nullable
            services.ThrowIfNullable();

            ValidatorOptions.LanguageManager.Enabled = false;

            services.AddTransient<IValidator<CreateCustomerCommand>, CreateCustomerCommandValidation>();

            services.AddTransient<IValidator<UpdateCustomerCommand>, UpdateCustomerCommandValidation>();
        }
    }
}
