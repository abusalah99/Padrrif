namespace Padrrif;

public interface IUnitOfWork<TEntity>
{
    Task Create(TEntity entity);
    Task<List<TEntity>> Read(Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null);
    Task<TEntity?> Read(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null);
    Task Update(TEntity entity);

}
