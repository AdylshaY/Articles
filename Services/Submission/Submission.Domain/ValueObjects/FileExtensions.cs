namespace Submission.Domain.ValueObjects
{
    using Blocks.Core.Extensions;
    using System.Collections.Generic;

    public class FileExtensions
    {
        public IReadOnlyList<string> Extensions { get; set; } = null!;

        // If the list of extensions is empty, it means that all extensions are valid.
        public bool IsValidExtension(string extension) => Extensions.IsEmpty() || Extensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
    }
}
