namespace Submission.Domain.Entities
{
    using Articles.Abstractions.Enums;
    using Blocks.Domain.Entities;

    public class Article : IEntity
    {
        public int Id { get; init; }
        public required string Title { get; set; }
        public required string Scope { get; set; }
        public required ArticleType Type { get; set; }
        public ArticleStage Stage { get; internal set; }
        public int JournalId { get; set; }
        public required Journal Journal { get; init; }
        public List<ArticleActor> Actors { get; set; } = [];
    }
}
