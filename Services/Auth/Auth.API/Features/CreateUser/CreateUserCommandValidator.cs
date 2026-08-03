namespace Auth.API.Features.CreateUser
{
    using FastEndpoints;
    using FluentValidation;

    public class CreateUserCommandValidator : Validator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.LastName).NotEmpty();

            RuleFor(c => c.Email).NotEmpty().EmailAddress();

            RuleFor(c => c.UserRoles).NotEmpty().Must((c, roles) => AreUserRoleDatesValid(roles)).WithMessage("Invalid Role");
        }

        private static bool AreUserRoleDatesValid(IEnumerable<UserRoleDto> roles)
        {
            return roles.All(r =>
            (!r.StartDate.HasValue || r.StartDate.Value.Date >= DateTime.UtcNow.Date) &&
            (!r.ExpiringDate.HasValue || (r.StartDate ?? DateTime.UtcNow).Date < r.ExpiringDate.Value.Date));
        }
    }
}
