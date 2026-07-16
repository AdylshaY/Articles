namespace Submission.Persistence.Repositories
{
    using Submission.Domain.Entities;

    public class ArticleRepository(SubmissionDbContext dbContext) : Repository<Article>(dbContext)
    {

    }
}
