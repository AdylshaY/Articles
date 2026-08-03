namespace Auth.Domain.Users.ValueObjects
{
    using Auth.Domain.Users.Enums;
    using Blocks.Core;
    using Blocks.Domain.ValueObjects;

    public class HonorificTitle : StringValueObject
    {
        private HonorificTitle(string value) => Value = value;

        public static HonorificTitle FromString(string value)
        {
            Guard.ThrowIfNullOrWhitespace(value);
            return new HonorificTitle(value.Trim());
        }

        public static HonorificTitle? FromEnum(Honorific? honorific)
        {
            if (honorific.HasValue) return new HonorificTitle(honorific.ToString());
            return null;
        }
    }
}
