namespace Submission.Persistence.EntityConfigurations
{
    using Blocks.EntityFramework;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Submission.Domain.Entities;

    internal class ArticleAuthorEntityConfiguration : IEntityTypeConfiguration<ArticleAuthor>
    {
        public void Configure(EntityTypeBuilder<ArticleAuthor> builder)
        {
            builder.Property(x => x.ContributionAreas).HasJsonCollectionConversion().IsRequired();
        }
    }
}
