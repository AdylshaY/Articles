namespace Auth.Domain.Users
{
    using Articles.Abstractions.Enums;
    using System;

    public interface IUserRole
    {
        DateTime? ExpiringDate { get; }
        DateTime? StartDate { get; }
        UserRoleType Type { get; }
    }
}
