namespace Submission.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Submission.Domain.Entities;

    public class SubmissionDbContext(DbContextOptions<SubmissionDbContext> options) : DbContext(options)
    {
        #region Entities
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Journal> Journals { get; set; }
        #endregion

        #region Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
        #endregion
    }
}
