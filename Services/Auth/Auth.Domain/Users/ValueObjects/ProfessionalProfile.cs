namespace Auth.Domain.Users.ValueObjects
{
    using Blocks.Domain.ValueObjects;

    public class ProfessionalProfile : ValueObject
    {
        public string? Position { get; private set; }
        public string? CompanyName { get; private set; }
        public string? Affiliation { get; private set; }

        private ProfessionalProfile() { }

        public static ProfessionalProfile Create(string? position, string? companyName, string? affiliation)
        {
            return new ProfessionalProfile
            {
                Position = string.IsNullOrWhiteSpace(position) ? null : position.Trim(),
                CompanyName = string.IsNullOrWhiteSpace(companyName) ? null : companyName.Trim(),
                Affiliation = string.IsNullOrWhiteSpace(affiliation) ? null : affiliation.Trim()
            };
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            // By using yield return, we can return each property one at a time, which is more efficient than creating a list and returning it.
            yield return Position;
            yield return CompanyName;
            yield return Affiliation;
        }

        public override string ToString() => $"{Position}{(string.IsNullOrEmpty(Position) || string.IsNullOrEmpty(CompanyName) ? "" : " at " + CompanyName)}{(string.IsNullOrEmpty(Affiliation) ? "" : " (" + Affiliation + ")")}";
    }
}
