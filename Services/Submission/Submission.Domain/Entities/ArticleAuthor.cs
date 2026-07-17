namespace Submission.Domain.Entities
{
    using Articles.Abstractions.Enums;

    public class ArticleAuthor : ArticleActor
    {
        public HashSet<ContributionArea> ContributionAreas { get; init; } = null!;
    }
}
