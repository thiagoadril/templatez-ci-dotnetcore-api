using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Templatez.Infra.CrossCutting.Extensions;
using Templatez.Infra.CrossCutting.Identity;

namespace Templatez.Api.Setup
{
    public static class AuthSetup
    {
        public static void AddIdentitySetup(this IServiceCollection services, IConfiguration configuration)
        {
            // Throw if object nullable
            services.ThrowIfNullable();
            configuration.ThrowIfNullable();

            //TODO: IdentitySetup
        }

        public static void AddAuthSetup(this IServiceCollection services, IConfiguration configuration)
        {
            // Throw if object nullable
            services.ThrowIfNullable();
            configuration.ThrowIfNullable();

            //TODO: AuthSetup

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            string domain = $"https://{configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = configuration["Auth0:ApiIdentifier"];
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:customer", policy => policy.Requirements.Add(new HasScopeRequirement("read:customer", domain)));
                options.AddPolicy("create:customer", policy => policy.Requirements.Add(new HasScopeRequirement("create:customer", domain)));
                options.AddPolicy("update:customer", policy => policy.Requirements.Add(new HasScopeRequirement("update:customer", domain)));
                options.AddPolicy("delete:customer", policy => policy.Requirements.Add(new HasScopeRequirement("delete:customer", domain)));
            });

            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        }

        public static void UseAuthSetup(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Throw if object nullable
            app.ThrowIfNullable();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();
        }
    }
}
