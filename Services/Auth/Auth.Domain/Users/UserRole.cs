namespace Auth.Domain.Users
{
    using Microsoft.AspNetCore.Identity;

    public partial class UserRole : IdentityUserRole<int>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? ExpiringDate { get; set; }
    }
}
