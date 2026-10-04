namespace Auth.Domain.Users
{
    using Auth.Domain.Users.Enums;
    using Auth.Domain.Users.ValueObjects;
    using Blocks.Domain.Entities;
    using Microsoft.AspNetCore.Identity;

    public partial class User : IdentityUser<int>, IEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public required Gender Gender { get; set; }
        public HonorificTitle? Honorific { get; set; }
        public ProfessionalProfile? ProfessionalProfile { get; set; }
        public string? PictureUrl { get; set; } = null!;
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastLogin { get; set; }

        private List<UserRole> _userRoles = new();
        public virtual IReadOnlyList<UserRole> UserRoles => _userRoles;
        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}
