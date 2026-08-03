namespace Auth.Domain.Roles
{
    using Articles.Abstractions.Enums;
    using Blocks.Domain.Entities;
    using Microsoft.AspNetCore.Identity;

    public class Role : IdentityRole<int>, IEntity
    {
        public required UserRoleType Type { get; set; }
        public required string Description { get; set; }
    }
}
