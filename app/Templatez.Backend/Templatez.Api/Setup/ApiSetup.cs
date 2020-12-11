using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Templatez.Api.Http.Errors;
using Templatez.Infra.CrossCutting.Extensions;

namespace Templatez.Api.Setup
{
    public static class ApiSetup
    {
        public static void AddApiSetup(this IServiceCollection services, IConfiguration configuration)
        {
            // Throw if object nullable
            services.ThrowIfNullable();
            configuration.ThrowIfNullable();

            // MVC Config
            services.AddMvcCore().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            }).AddApiExplorer();

            // WebAPI Config
            services
                .AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        return new UnprocessableEntityObjectResult(new ErrorRequestModelValidation(context));
                    };
                });

            // Api Version
            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });
        }

        public static void UseApiSetup(this IApplicationBuilder app)
        {
            // Throw if object nullable
            app.ThrowIfNullable();

            // Handles exceptions and generates a custom response body
            app.UseExceptionHandler("/error/500");

            // Handles non-success status codes with empty body
            app.UseStatusCodePagesWithReExecute("/error/{0}");
        }
    }
}