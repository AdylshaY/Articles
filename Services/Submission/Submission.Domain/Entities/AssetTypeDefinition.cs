namespace Submission.Domain.Entities
{
    using Blocks.Core.Cache;
    using Blocks.Domain.Entities;

    public class AssetTypeDefinition : EnumEntity<AssetType>, ICacheable
    {
        public required byte MaxFileSizeInMB { get; set; }
        public int MaxFileSizeInBytes => MaxFileSizeInMB * 1024 * 1024;
        public required string DefaultFileExtension { get; set; } = default!;
        public required FileExtensions AllowedFileExtensions { get; set; } = default!;
        public int MaxAssetCount { get; init; }
        public bool AllowMultipleAssets => MaxAssetCount > 1;
    }
}
