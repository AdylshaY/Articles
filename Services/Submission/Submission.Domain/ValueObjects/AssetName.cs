namespace Submission.Domain.ValueObjects
{
    using Articles.Abstractions.Enums;
    using Blocks.Domain.ValueObjects;

    public class AssetName : StringValueObject
    {
        private AssetName(string value) => Value = value;

        public static AssetName FromAssetType(AssetTypeDefinition assetType) => new(assetType.Name.ToString());
    }
}
