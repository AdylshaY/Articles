namespace Submission.Persistence.Repositories
{
    using Blocks.Domain.Entities;
    using Blocks.EntityFramework;

    public class Repository<TEntity>(SubmissionDbContext dbContext): Repository<SubmissionDbContext, TEntity>(dbContext)
        where TEntity : class, IEntity
    {
    }
}
