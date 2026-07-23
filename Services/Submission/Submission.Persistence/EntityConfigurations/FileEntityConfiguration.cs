namespace Submission.Persistence.EntityConfigurations
{
    using Blocks.Core.Constraints;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Submission.Domain.ValueObjects;

    internal class FileEntityConfiguration
    {
        public void Configure(ComplexPropertyBuilder<File> builder)
        {
            builder.Property(e => e.OriginalName).HasMaxLength(MaxLength.C256).HasComment("Original full file name, with extension.");
            builder.Property(e => e.FileServerId).HasMaxLength(MaxLength.C64);
            builder.Property(e => e.Size).HasComment("File size in kilobytes.");

            builder.ComplexProperty(o => o.Extension, complexBuilder =>
            {
                complexBuilder.Property(n => n.Value)
                              .HasColumnName($"{builder.Metadata.ClrType.Name}_{complexBuilder.Metadata.PropertyInfo!.Name}")
                              .HasMaxLength(MaxLength.C8);
            });

            builder.ComplexProperty(o => o.Name, complexBuilder =>
            {
                complexBuilder.Property(n => n.Value)
                              .HasColumnName($"{builder.Metadata.ClrType.Name}_{complexBuilder.Metadata.PropertyInfo!.Name}")
                              .HasMaxLength(MaxLength.C64)
                              .HasComment("Final name of the file after renaming.");
            });
        }
    }
}
