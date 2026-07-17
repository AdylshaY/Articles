namespace Submission.Persistence.EntityConfigurations
{
    using Submission.Domain.Entities;
    using Blocks.EntityFramework.EntityConfigurations;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;

    internal class PersonEntityConfiguration : EntityConfiguration<Person>
    {
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            base.Configure(builder);

            builder.HasIndex(e => e.UserId).IsUnique();

            builder.HasDiscriminator(e => e.TypeDiscriminator)
                .HasValue<Person>(nameof(Person))
                .HasValue<Author>(nameof(Author));

            #region FirstName
            builder
                .Property(e => e.FirstName)
                .HasMaxLength(64)
                .IsRequired();
            #endregion

            #region LastName
            builder
                .Property(e => e.LastName)
                .HasMaxLength(64)
                .IsRequired();
            #endregion

            #region Title
            builder
                .Property(e => e.Title)
                .HasMaxLength(64);
            #endregion

            #region Affiliation
            builder
                .Property(e => e.Affiliation)
                .HasMaxLength(512)
                .IsRequired()
                .HasComment("Institution or organization they are associated with when they conduct their research.");
            #endregion

            #region UserId
            builder
                .Property(e => e.UserId)
                .IsRequired(false);
            #endregion

            #region EmailAddress
            builder.ComplexProperty(o => o.EmailAddress, builder =>
            {
                builder
                    .Property(n => n.Value)
                    .HasColumnName(builder.Metadata.PropertyInfo!.Name)
                    .HasMaxLength(64);
            });
            #endregion
        }
    }
}
