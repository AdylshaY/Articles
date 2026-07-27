namespace Submission.Domain.ValueObjects
{
    using Blocks.Domain.ValueObjects;
    using Submission.Domain.Entities;

    public class FileName : StringValueObject
    {
        private FileName(string value) => Value = value;

        public static FileName FromAsset(Asset asset, string extension)
        {
            var assetName = asset.Name.Value;
            return new FileName($"{assetName}.{extension}");
        }
    }
}
