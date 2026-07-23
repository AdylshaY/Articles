namespace Submission.Persistence.EntityConfigurations
{
    using Blocks.Core.Constraints;
    using Blocks.EntityFramework;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Submission.Domain.Entities;

    internal class AssetTypeDefinitionEntityConfiguration : IEntityTypeConfiguration<AssetTypeDefinition>
    {
        public void Configure(EntityTypeBuilder<AssetTypeDefinition> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Name).IsUnique();

            builder.Property(e => e.Name)
                .HasEnumConversion()
                .HasMaxLength(MaxLength.C64)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(e => e.MaxFileSizeInMB).HasDefaultValue(5);

            builder.Property(e => e.DefaultFileExtension).HasMaxLength(MaxLength.C8).HasDefaultValue("pdf").IsRequired();

            builder.ComplexProperty(o => o.AllowedFileExtensions, builder =>
            {
                var convertor = BuilderExtensions.BuildJsonListConvertor<string>();
                builder.Property(o => o.Extensions)
                       .HasConversion(convertor)
                       .HasColumnName(builder.Metadata.PropertyInfo!.Name)
                       .IsRequired();
            });
        }
    }
}
