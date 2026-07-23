namespace Submission.Domain.Entities
{
    using Blocks.Domain.Entities;

    public partial class Journal : Entity
    {
        private readonly List<Article> _articles = [];

        public required string Name { get; set; }
        public required string Abreviation { get; set; }
        public IList<Article> Articles => _articles.AsReadOnly();
    }
}
