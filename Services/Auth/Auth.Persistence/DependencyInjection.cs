namespace Auth.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("Database");
            services.AddDbContext<AuthDBContext>(opts => opts.UseSqlServer(connectionString));

            return services;
        }
    }
}
