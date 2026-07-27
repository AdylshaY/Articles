namespace FileStorage.Contracts
{
    using Microsoft.AspNetCore.Http;

    public interface IFileService
    {
        Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, bool overwrite = false, Dictionary<string, string>? tags = null);
        Task<(Stream FileStream, string ContentType)> DownloadFileAsync(string fileId);
        Task<bool> TryDeleteFileAsync(string fileId);
    }
}
