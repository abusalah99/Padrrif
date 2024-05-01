namespace Padrrif;

public interface IUnitOfWork<TEntity>
{
    Task Create(TEntity entity);
    Task<List<TEntity>> Read();
    Task<TEntity?> Read(Guid id);
}
