namespace Blocks.EntityFramework
{
    using Blocks.Domain.Entities;
    using Blocks.Exceptions;
    using Microsoft.EntityFrameworkCore;

    public static class RepositoryExtensions
    {
        public static async Task<TEntity> FindByIdOrThrowAsync<TEntity, TContext>(this Repository<TContext, TEntity> repository, int id)
            where TContext : DbContext
            where TEntity : class, IEntity
        {
            var entity = await repository.FindByIdAsync(id) ?? throw new NotFoundException($"{typeof(TEntity).Name} cannot be found");
            return entity;
        }
    }
}
