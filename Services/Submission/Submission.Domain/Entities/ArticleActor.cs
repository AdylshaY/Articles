namespace Submission.Domain.Entities
{
    using Articles.Abstractions.Enums;

    public class ArticleActor
    {
        public int ArticleId { get; init; }
        public int PersonId { get; init; }

        public Article Article { get; init; } = null!;
        public Person Person { get; init; } = null!;

        public UserRoleType Role { get; init; }

        public string TypeDiscriminator { get; init; } = null!; // EF Discriminator
    }
}
