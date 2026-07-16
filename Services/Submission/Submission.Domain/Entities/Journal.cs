namespace Submission.Domain.Entities
{
    using Blocks.Domain.Entities;

    public partial class Journal : IEntity
    {
        private readonly List<Article> _articles = [];

        public int Id { get; init; }
        public required string Name { get; set; }
        public required string Abreviation { get; set; }
        public IList<Article> Articles => _articles.AsReadOnly();
    }
}
