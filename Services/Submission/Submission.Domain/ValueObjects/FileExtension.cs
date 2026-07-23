namespace Submission.Domain.ValueObjects
{
    using Blocks.Core;
    using Blocks.Domain.ValueObjects;

    public class FileExtension : StringValueObject
    {
        private FileExtension(string value) => Value = value;

        public static FileExtension FromFileName(string fileName, AssetTypeDefinition assetType)
        {
            var extension = Path.GetExtension(fileName).Remove(0, 1);

            Guard.ThrowIfNullOrWhitespace(extension);
            Guard.ThrowIfNotEqual(assetType.AllowedFileExtensions.IsValidExtension(extension), true);

            return new FileExtension(extension);
        }
    }
}
