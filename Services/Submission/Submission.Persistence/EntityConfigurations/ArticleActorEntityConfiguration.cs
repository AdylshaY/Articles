namespace Submission.Persistence.EntityConfigurations
{
    using Articles.Abstractions.Enums;
    using Blocks.EntityFramework;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Submission.Domain.Entities;

    internal class ArticleActorEntityConfiguration : IEntityTypeConfiguration<ArticleActor>
    {
        public void Configure(EntityTypeBuilder<ArticleActor> builder)
        {
            builder.HasKey(e => new { e.ArticleId, e.PersonId, e.Role });

            builder.HasDiscriminator(e => e.TypeDiscriminator)
                   .HasValue<ArticleActor>(nameof(ArticleActor))
                   .HasValue<ArticleAuthor>(nameof(ArticleAuthor));

            #region Role
            builder.Property(e => e.Role)
                   .HasEnumConversion()
                   .HasDefaultValue(UserRoleType.AUT);
            #endregion

            #region Article
            builder.HasOne(aa => aa.Article)
                   .WithMany(a => a.Actors)
                   .HasForeignKey(aa => aa.ArticleId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Person
            builder.HasOne(aa => aa.Person)
                   .WithMany(p => p.ArticleActors)
                   .HasForeignKey(aa => aa.PersonId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
