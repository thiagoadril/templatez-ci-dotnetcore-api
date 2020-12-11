using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Templatez.Api.Controllers.Mappers.Customers;
using Templatez.Infra.CrossCutting.Extensions;

namespace Templatez.Api.Setup
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            // Throw if object nullable
            services.ThrowIfNullable();

            services.AddAutoMapper(typeof(CustomerApplicationToControllerMapper), typeof(CustomerControllerToApplicationMapper));
        }
    }
}
