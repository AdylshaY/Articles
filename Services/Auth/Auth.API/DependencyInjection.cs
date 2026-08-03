namespace Auth.API
{
    using Articles.Security;
    using Auth.Domain.Roles;
    using Auth.Domain.Users;
    using Auth.Persistence;
    using EmailService.Smtp;
    using FastEndpoints;
    using FastEndpoints.Swagger;
    using Microsoft.AspNetCore.Identity;
    using System.Security.Claims;

    public static class DependencyInjection
    {
        public static void ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
        {
            // Use it for configure options
        }

        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddFastEndpoints()
                .SwaggerDocument()
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddJwtAuthentication(configuration)
                .AddJwtIdentity(configuration)
                .AddAuthorization();

            services.AddSmtpEmailService(configuration);

            return services;
        }

        public static IServiceCollection AddJwtIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<User>(options =>
            {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<AuthDBContext>()
            .AddSignInManager<SignInManager<User>>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
            });

            return services;
        }
    }
}
