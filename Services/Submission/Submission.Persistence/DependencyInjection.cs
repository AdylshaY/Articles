namespace Submission.Persistence
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Submission.Persistence.Repositories;

    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SubmissionDbContext>((provider, options) =>
            {

            });

            services.AddScoped(typeof(Repository<>));
            services.AddScoped(typeof(ArticleRepository));

            return services;
        }
    }
}
