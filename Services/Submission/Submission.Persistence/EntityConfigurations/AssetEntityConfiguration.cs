namespace Submission.Persistence.EntityConfigurations
{
    using Blocks.Core.Constraints;
    using Blocks.EntityFramework;
    using Blocks.EntityFramework.EntityConfigurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Submission.Domain.Entities;

    internal class AssetEntityConfiguration : EntityConfiguration<Asset>
    {
        public override void Configure(EntityTypeBuilder<Asset> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Type).HasEnumConversion();

            builder.ComplexProperty(o => o.Name, builder =>
            {
                builder.Property(e => e.Value)
                        .HasColumnName(builder.Metadata.PropertyInfo!.Name)
                        .HasMaxLength(MaxLength.C64)
                        .IsRequired();
            });

            builder.ComplexProperty(o => o.File, fileBuilder =>
            {
                new FileEntityConfiguration().Configure(fileBuilder);
            });
        }
    }
}
