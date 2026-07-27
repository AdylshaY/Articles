namespace Blocks.Core
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddAndValidateOptions<TOptions>(this IServiceCollection services, IConfiguration configuration)
            where TOptions : class
        {
            var section = configuration.GetSection(typeof(TOptions).Name);
            if (!section.Exists()) throw new InvalidOperationException($"Configuration section '{section.Key}' is missing.");

            services
                .AddOptions<TOptions>()
                .Bind(section)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services;
        }

        public static T GetSectionByTypeName<T>(this IConfiguration configuration)
        {
            var sectionName = typeof(T).Name;
            var section = configuration.GetSection(sectionName).Get<T>();
            return Guard.AgainstNull(section, sectionName);
        }

        public static string GetConnectionStringOrThrow(this IConfiguration configuration, string name)
        {
            var connectionString = configuration.GetConnectionString(name);
            if (string.IsNullOrWhiteSpace(connectionString)) throw new InvalidOperationException($"Connection string '{name}' is missing or empty.");
            return connectionString;
        }
    }
}
