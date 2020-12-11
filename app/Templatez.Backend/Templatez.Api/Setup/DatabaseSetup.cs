using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Templatez.Domain.Core.Settings.Database;
using Templatez.Domain.Repositories;
using Templatez.Infra.CrossCutting.Extensions;
using Templatez.Infra.Data.Core.Contexts;
using Templatez.Infra.Data.Repositories;
using System.Linq;

namespace Templatez.Api.Setup
{
    public static class DatabaseSetup
    {
        public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            // Throw if object nullable
            services.ThrowIfNullable();
            configuration.ThrowIfNullable();

            //Settings
            services.Configure<DatabaseSettings>(options => configuration.GetSection("Database").Bind(options));

            //Connectors

            //Contexts
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetSection("Database:PostgreSQL:ConnectionString").Value,
                x => x.MigrationsAssembly("Templatez.Infra.Data.Core")));

            //Repositories
            services.AddTransient<ICustomerRepository, CustomerRepository>();
        }

        public static void UseDatabaseSetup(this IApplicationBuilder app, IWebHostEnvironment env, AppDbContext context)
        {
            app.ThrowIfNullable();
            env.ThrowIfNullable();
            context.ThrowIfNullable();

            try
            {
                var hasPendingMigrations = context.Database.GetPendingMigrations().Count() > 0;
                if (hasPendingMigrations)
                    context.Database.Migrate();
            }
            catch { }
        }
    }
}
