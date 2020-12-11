using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Templatez.Api.Setup;
using Templatez.Infra.Data.Core.Contexts;
using Serilog;

namespace Templatez.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Logger
            services.AddSingleton(Log.Logger);

            // Datatase
            services.AddDatabaseSetup(Configuration);

            // Auth 
            services.AddAuthSetup(Configuration);

            // API 
            services.AddApiSetup(Configuration);

            // Validation dependes AddControllers
            services.AddValidatorSetup();

            // Swagger
            services.AddSwaggerSetup(Configuration);

            // AutoMapper
            services.AddAutoMapperSetup();

            // DI
            services.AddDependencyInjectionSetup();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, AppDbContext context)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSerilogRequestLogging();

            app.UseDatabaseSetup(env, context);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseAuthSetup(env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerSetup(env, provider);
        }
    }
}
