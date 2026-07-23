namespace Submission.Application.Features.UploadFile.UploadManuscriptFile
{
    using Blocks.EntityFramework;
    using System.Threading.Tasks;

    public class UploadManuscriptFileCommandHandler(ArticleRepository _articleRepository, Repository<AssetTypeDefinition> _assetTypeRepository) : IRequestHandler<UploadManuscriptFileCommand, IdResponse>
    {
        public async Task<IdResponse> Handle(UploadManuscriptFileCommand command, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

            var assetType = await _assetTypeRepository.FindByIdAsync((int)command.AssetType);

            Asset asset = null;
            if (!assetType.AllowMultipleAssets) asset = article.Assets.SingleOrDefault(a => a.Type == assetType.Id);

            if (asset is null) asset = article.CreateAsset(assetType);

            //TODO: Upload the file to the storage and set the file property of the asset

            await _articleRepository.SaveChangesAsync(cancellationToken);

            return new IdResponse(asset.Id);
        }
    }
}
