namespace Submission.API
{
    using FileStorage.MongoGridFS;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache()
                    .AddEndpointsApiExplorer()
                    .AddSwaggerGen();

            services.AddMongoFileStorage(configuration);

            return services;
        }
    }
}
