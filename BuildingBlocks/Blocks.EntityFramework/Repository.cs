namespace Blocks.EntityFramework
{
    using Blocks.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task<TEntity?> FindByIdAsync(int id);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        void Remove(TEntity entity);
        Task<bool> DeleteByIdAsync(int id);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    public class Repository<TContext, TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        protected readonly TContext _dbContext;
        protected readonly DbSet<TEntity> _entity;

        public Repository(TContext dbContext)
        {
            _dbContext = dbContext;
            _entity = _dbContext.Set<TEntity>();
        }

        public TContext Context => _dbContext;
        public virtual DbSet<TEntity> Entity => _entity;
        protected virtual IQueryable<TEntity> Query() => _entity;

        public virtual async Task<TEntity?> GetByIdAsync(int id) => await Query().SingleOrDefaultAsync(e => e.Id.Equals(id));

        public virtual async Task<TEntity> AddAsync(TEntity entity) => (await _entity.AddAsync(entity)).Entity;

        public virtual TEntity Update(TEntity entity) => _entity.Update(entity).Entity;

        public virtual void Remove(TEntity entity) => _entity.Remove(entity);

        public virtual async Task<bool> DeleteByIdAsync(int id) => await _dbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM {_entity.EntityType.GetTableName()} WHERE Id = {id}") > 0;

        public virtual async Task<TEntity?> FindByIdAsync(int id) => await _entity.FindAsync(id);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
