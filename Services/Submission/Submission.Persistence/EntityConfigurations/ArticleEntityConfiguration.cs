namespace Submission.Persistence.EntityConfigurations
{
    using Blocks.EntityFramework;
    using Blocks.EntityFramework.EntityConfigurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Submission.Domain.Entities;

    internal class ArticleEntityConfiguration : EntityConfiguration<Article>
    {
        public override void Configure(EntityTypeBuilder<Article> builder)
        {
            base.Configure(builder);

            #region Title
            builder.Property(e => e.Title)
                .HasMaxLength(256)
                .IsRequired();
            #endregion

            #region Scope
            builder.Property(e => e.Scope)
                .HasMaxLength(2048)
                .IsRequired();
            #endregion

            #region Stage
            builder.Property(e => e.Stage)
                .HasEnumConversion();
            #endregion

            #region Type
            builder.Property(e => e.Type)
                .HasEnumConversion();
            #endregion

            #region Journal
            builder.HasOne(e => e.Journal)
                .WithMany(e => e.Articles)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
