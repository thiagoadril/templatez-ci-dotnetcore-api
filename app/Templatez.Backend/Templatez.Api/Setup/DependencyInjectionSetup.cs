using Microsoft.Extensions.DependencyInjection;
using Templatez.Application.Services.Customers;
using Templatez.Domain.Interfaces.Services.Customers;
using Templatez.Domain.Services;
using Templatez.Infra.CrossCutting.Extensions;

namespace Templatez.Api.Setup
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjectionSetup(this IServiceCollection services)
        {
            // Throw if object nullable
            services.ThrowIfNullable();

            //TODO: DI
            services.AddScoped<ICustomersService, CustomersService>();
            services.AddScoped<ICustomerAppService, CustomerAppService>();
        }
    }
}
