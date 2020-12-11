using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Templatez.Domain.Core.Settings.Swagger;
using Templatez.Infra.CrossCutting.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Templatez.Api.Setup
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services, IConfiguration configuration)
        {
            // Throw if object nullable
            services.ThrowIfNullable();
            configuration.ThrowIfNullable();

            services.Configure<SwaggerSettings>(configuration.GetSection("Swagger"));
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider)
        {

            // Throw if object nullable
            app.ThrowIfNullable();
            env.ThrowIfNullable();
            provider.ThrowIfNullable();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        $"{description.GroupName.ToUpperInvariant()} - {env.EnvironmentName}");
                }

                options.DocExpansion(DocExpansion.List);
            });
        }

        private class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
        {
            readonly IApiVersionDescriptionProvider _provider;
            readonly IWebHostEnvironment _env;
            readonly IOptions<SwaggerSettings> _options;

            public ConfigureSwaggerOptions(
                IApiVersionDescriptionProvider provider,
                IWebHostEnvironment env,
                IOptions<SwaggerSettings> options)
            {
                _provider = provider;
                _env = env;
                _options = options;
            }

            public void Configure(SwaggerGenOptions options)
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                            Reference = new OpenApiReference
                                {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                            },
                            new List<string>()
                        }
                    });

                foreach (var avd in _provider.ApiVersionDescriptions)
                {
                    var version = $"v{avd.ApiVersion}";
                    var description =
                        $"{_options.Value?.Description}" +
                        $"<br/><br/>" +
                        $"The version is running in a {_env.EnvironmentName.ToLower()} environment.";

                    options.SwaggerDoc(version,
                        new OpenApiInfo
                        {
                            Title = $"{_options.Value?.Name} API",
                            Version = version,
                            Description = description,
                            Contact = new OpenApiContact
                            {
                                Name = _options.Value?.Contact?.Name,
                                Url = new Uri(_options.Value?.Contact?.URL)
                            }
                        });

                    //options.AddFluentValidationRules();
                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                    if (File.Exists(xmlPath))
                        options.IncludeXmlComments(xmlPath);
                }
            }
        }
    }
}
