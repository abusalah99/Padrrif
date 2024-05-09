namespace Padrrif;

public interface IRepository<TEntity>
{
    Task<bool> Add(TEntity entity);
    Task<List<TEntity>> GetList(Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null);
    Task<TEntity?> GetSingleEntityWithSomeCondiition(Func<IQueryable<TEntity>, IQueryable<TEntity>> additionalQuery);
    Task<TEntity?> GetById(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null);
    Task<int> GetTableRecordsCount(Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null);
    Task<List<TResult>> SelectListOfProperty<TResult>(Func<TEntity, TResult> propertySelector,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null);
    Task<TResult?> GetSinglePropertyValue<TResult>(Func<TEntity, TResult> propertySelector,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null);
    Task<bool> HardUpdateEntity(TEntity entity);
    Task<bool> Update(TEntity entity);
    Task<bool> Remove(Guid id);
    Task<bool> Remove(TEntity entity);
    Task<IDbContextTransaction> GetTransaction();
}
