namespace Auth.Persistence.EntityConfigurations
{
    using Auth.Domain.Roles;
    using Blocks.Core.Constraints;
    using Blocks.EntityFramework;
    using Blocks.EntityFramework.EntityConfigurations;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class RoleEntityConfiguration : EntityConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Type).IsRequired().HasEnumConversion();
            builder.Property(x => x.Description).IsRequired().HasMaxLength(MaxLength.C256);
        }
    }
}
