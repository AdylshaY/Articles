namespace Submission.Domain.ValueObjects
{
    using Blocks.Core;
    using Blocks.Domain.ValueObjects;
    using System.Text.RegularExpressions;

    public class EmailAddress : StringValueObject
    {
        private EmailAddress(string value) => Value = value;

        public static EmailAddress Create(string value)
        {
            Guard.ThrowIfNullOrWhitespace(value);
            if (!IsValidEmail(value))
            {
                throw new ArgumentException("Invalid email address format.", nameof(value));
            }
            return new EmailAddress(value);
        }

        private static bool IsValidEmail(string email)
        {
            const string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase);
        }
    }
}
