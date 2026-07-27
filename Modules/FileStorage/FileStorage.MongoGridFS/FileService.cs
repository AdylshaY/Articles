namespace FileStorage.MongoGridFS
{
    using FileStorage.Contracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.GridFS;

    public class FileService : IFileService
    {
        private readonly GridFSBucket _bucket;
        private readonly MongoGridFsFileStorageOptions _options;

        private const string DefaultContentType = "application/octet-stream";
        private const string FilePathMetadataKey = "filePath";
        private const string ContentTypeMetadataKey = "contentType";

        public FileService(GridFSBucket bucket, IOptions<MongoGridFsFileStorageOptions> options)
        {
            _bucket = bucket;
            _options = options.Value;
        }

        public async Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, bool overwrite = false, Dictionary<string, string>? tags = null)
        {
            if (file.Length > _options.FileSizeLimitInBytes) throw new InvalidOperationException($"File size exceeds the limit of {_options.FileSizeLimitInMB} MB.");

            var metadata = new BsonDocument(tags ?? new Dictionary<string, string>()
            {
                { FilePathMetadataKey, filePath },
                { ContentTypeMetadataKey, file.ContentType }
            });

            var uploadOptions = new GridFSUploadOptions
            {
                Metadata = metadata,
                ChunkSizeBytes = _options.ChunkSizeBytes
            };

            using var stream = file.OpenReadStream();
            ObjectId fileId = await _bucket.UploadFromStreamAsync(file.FileName, stream, uploadOptions);

            return new UploadResponse(
                FilePath: filePath,
                FileName: file.FileName,
                FileSize: file.Length,
                FileId: fileId.ToString()
            );
        }

        public async Task<(Stream FileStream, string ContentType)> DownloadFileAsync(string fileId)
        {
            if (!ObjectId.TryParse(fileId, out ObjectId objectId)) throw new FileNotFoundException("Invalid file ID format.", nameof(fileId));

            var fileInfo = await _bucket.Find(Builders<GridFSFileInfo>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync() ?? throw new FileNotFoundException("File not found.", nameof(fileId));

            var stream = await _bucket.OpenDownloadStreamAsync(objectId);
            var contentType = fileInfo.Metadata?.GetValue(ContentTypeMetadataKey, DefaultContentType)?.AsString ?? DefaultContentType;
            return (stream, contentType);
        }

        public async Task<bool> TryDeleteFileAsync(string fileId)
        {
            if (!ObjectId.TryParse(fileId, out ObjectId objectId)) return false;

            try
            {
                await _bucket.DeleteAsync(objectId);
                return true;
            }
            catch (GridFSFileNotFoundException)
            {
                return false;
            }
        }
    }
}
