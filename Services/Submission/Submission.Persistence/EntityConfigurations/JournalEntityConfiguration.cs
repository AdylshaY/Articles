namespace Submission.Persistence.EntityConfigurations
{
    using Blocks.EntityFramework.EntityConfigurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Submission.Domain.Entities;

    internal class JournalEntityConfiguration : EntityConfiguration<Journal>
    {
        public override void Configure(EntityTypeBuilder<Journal> builder)
        {
            base.Configure(builder);

            #region Name
            builder.Property(e => e.Name)
                .HasMaxLength(64)
                .IsRequired();
            #endregion

            #region Abreviation
            builder.Property(e => e.Abreviation)
                .HasMaxLength(8)
                .IsRequired();
            #endregion
        }
    }
}
