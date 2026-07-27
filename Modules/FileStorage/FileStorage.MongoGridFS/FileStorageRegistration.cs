namespace FileStorage.MongoGridFS
{
    using Blocks.Core;
    using FileStorage.Contracts;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MongoDB.Driver;
    using MongoDB.Driver.GridFS;

    public static class FileStorageRegistration
    {
        public static IServiceCollection AddMongoFileStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAndValidateOptions<MongoGridFsFileStorageOptions>(configuration);
            var options = configuration.GetSectionByTypeName<MongoGridFsFileStorageOptions>();

            services.AddSingleton<IMongoClient>(sp =>
            {
                return new MongoClient(configuration.GetConnectionStringOrThrow(options.ConnectionStringName));
            });

            services.AddSingleton(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(options.DatabaseName);
            });

            services.AddSingleton(sp =>
            {
                var database = sp.GetRequiredService<IMongoDatabase>();
                return new GridFSBucket(database, new GridFSBucketOptions
                {
                    BucketName = options.BucketName,
                    ChunkSizeBytes = options.ChunkSizeBytes,
                    WriteConcern = WriteConcern.WMajority,
                    ReadPreference = ReadPreference.Primary
                });
            });

            services.AddSingleton<IFileService, FileService>();

            return services;
        }
    }
}
