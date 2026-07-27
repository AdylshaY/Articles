namespace Submission.Application.Features.UploadFile.UploadManuscriptFile
{
    using Blocks.EntityFramework;
    using FileStorage.Contracts;
    using System.Threading.Tasks;

    public class UploadManuscriptFileCommandHandler(ArticleRepository _articleRepository, AssetTypeDefinitionRepository _assetTypeRepository, IFileService _fileService) : IRequestHandler<UploadManuscriptFileCommand, IdResponse>
    {
        public async Task<IdResponse> Handle(UploadManuscriptFileCommand command, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

            var assetType = _assetTypeRepository.GetById(command.AssetType);

            Asset asset = null;
            if (!assetType.AllowMultipleAssets) asset = article.Assets.SingleOrDefault(a => a.Type == assetType.Id);

            if (asset is null) asset = article.CreateAsset(assetType);

            var filePath = asset.GenerateStorageFilePath(command.File.FileName);

            var uploadResponse = await _fileService.UploadFileAsync(filePath, command.File, overwrite: true, tags: new Dictionary<string, string>
            {
                { "entity", nameof(Asset) },
                { "entityId", asset.Id.ToString() },
            });

            try
            {
                asset.CreateFile(uploadResponse, assetType);
                await _articleRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                await _fileService.TryDeleteFileAsync(uploadResponse.FileId);
                throw;
            }

            return new IdResponse(asset.Id);
        }
    }
}
