namespace Auth.Domain.Users
{
    using Auth.Domain.Users.ValueObjects;
    using Blocks.Core.Extensions;

    public partial class User
    {
        public static User Create(IUserCreationInfo userCreationInfo)
        {
            if (userCreationInfo.UserRoles.IsNullOrEmpty()) throw new ArgumentException("User must have at least one role.", nameof(userCreationInfo.UserRoles));

            var user = new User
            {
                UserName = userCreationInfo.Email,
                Email = userCreationInfo.Email,
                FirstName = userCreationInfo.FirstName,
                LastName = userCreationInfo.LastName,
                Gender = userCreationInfo.Gender,
                PhoneNumber = userCreationInfo.PhoneNumber,
                PictureUrl = userCreationInfo.PictureUrl,
                Honorific = HonorificTitle.FromEnum(userCreationInfo.Honorific),
                ProfessionalProfile = ProfessionalProfile.Create(userCreationInfo.Position, userCreationInfo.CompanyName, userCreationInfo.Affiliation),
                _userRoles = [.. userCreationInfo.UserRoles.Select(UserRole.Create)],
            };

            // Create domain event
            return user;
        }
    }
}
