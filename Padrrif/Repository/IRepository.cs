namespace Padrrif;

public interface IRepository<TEntity, TIdType>
{
    Task<bool> Add(TEntity entity);
    Task<List<TEntity>> GetList(Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null,
        Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false);
    Task<TEntity?> GetSingleEntityWithSomeCondiition(Func<IQueryable<TEntity>, IQueryable<TEntity>> additionalQuery,
        Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false);
    Task<TEntity?> GetById(Func<TEntity, TIdType> idSelector, TIdType id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null,
        Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false);
    Task<int> GetTableRecordsCount(Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null,
        Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false);
    Task<List<TResult>> SelectListOfProperty<TResult>(Func<TEntity, TResult> propertySelector,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null, Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false);
    Task<TResult?> GetSinglePropertyValue<TResult>(Func<TEntity, TResult> propertySelector,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null, Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false);
    Task<bool> HardUpdateEntity(TEntity entity, Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false);
    Task<bool> Update(Func<TEntity, TIdType> idSelector, TEntity entity, Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false);
    Task<bool> Remove(Func<TEntity, TIdType> idSelector, TIdType id, Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false);
    Task<bool> Remove(Func<TEntity, TIdType> idSelector, TEntity entity,
            Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false);
    Task<IDbContextTransaction> GetTransaction();
}
