namespace Submission.Application.Features.UploadFile.UploadManuscriptFile
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public record UploadManuscriptFileCommand : ArticleCommand
    {
        /// <summary>
        /// The asset type of the file being uploaded.
        /// </summary>
        [Required]
        public AssetType AssetType { get; init; }

        /// <summary>
        /// The file to be uploaded.
        /// </summary>
        [Required]
        public IFormFile File { get; init; } = null!;

        public override ArticleActionType ActionType => ArticleActionType.Upload;
    }

    public class UploadManuscriptFileCommandValidator : AbstractValidator<UploadManuscriptFileCommand>
    {
        public UploadManuscriptFileCommandValidator()
        {
            RuleFor(x => x.File).NotNullWithMessage();

            //TODO: Add validation for file size and type

            RuleFor(x => x.AssetType).Must(IsAssetTypeAllowed).WithMessage(c => $"Asset type '{c.AssetType}' is not allowed.");
        }

        private bool IsAssetTypeAllowed(AssetType assetType) => AllowedAssetTypes.Contains(assetType);

        private static IReadOnlyCollection<AssetType> AllowedAssetTypes => new HashSet<AssetType>() { AssetType.Manuscript };
    }
}
