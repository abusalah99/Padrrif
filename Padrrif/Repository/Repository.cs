namespace Padrrif;
public class Repository<TEntity, TIdType> : IRepository<TEntity, TIdType> where TEntity : class where TIdType : struct
{
    private readonly ApplicationDbContext _context;
    protected DbSet<TEntity> dbSet;
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<List<TEntity>> GetList(Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null, Func<TEntity, bool>? isDeleteSelector = null,
        bool notDeletedFlagValue = false)
    {
        IQueryable<TEntity> query = dbSet.AsNoTracking().AsQueryable();

        if (additionalQuery != null)
            query = additionalQuery(query);

        var entities = await query.ToListAsync();

        if (isDeleteSelector != null)
            entities = entities.Where(entity => isDeleteSelector(entity) == notDeletedFlagValue).ToList();

        return entities;
    }
    public virtual async Task<TEntity?> GetById(Func<TEntity, TIdType> idSelector, TIdType id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null,
        Func<TEntity, bool>? isDeleteSelector = null,
        bool notDeletedFlagValue = false)
    {
        IQueryable<TEntity> query = dbSet.AsNoTracking().AsQueryable();

        if (additionalQuery != null)
            query = additionalQuery(query);

        var entities = await query.ToListAsync();

        if (isDeleteSelector != null)
            entities = entities.Where(entity => isDeleteSelector(entity) == notDeletedFlagValue).ToList();

        return entities.FirstOrDefault(entity => idSelector(entity).Equals(id));
    }
    public virtual async Task<TEntity?> GetSingleEntityWithSomeCondiition(Func<IQueryable<TEntity>, IQueryable<TEntity>> additionalQuery,
        Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false)
    {
        var query = dbSet.AsNoTracking().AsQueryable();

        if (isDeleteSelector != null)
        {
            var entities = await query.ToListAsync();

            entities = entities.Where(entity => isDeleteSelector(entity) == notDeletedFlagValue).ToList();

            query = entities.AsQueryable();
        }

        query = additionalQuery(query);

        return await query.FirstOrDefaultAsync();
    }

    public virtual async Task<List<TResult>> SelectListOfProperty<TResult>(Func<TEntity, TResult> propertySelector,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null,
        Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false)
    {
        IQueryable<TEntity> query = dbSet.AsNoTracking().AsQueryable();

        if (isDeleteSelector != null)
            query = query.Where(entity => isDeleteSelector(entity) == notDeletedFlagValue);

        if (additionalQuery != null)
            query = additionalQuery(query);

        return await query.Select(entity => propertySelector(entity)).ToListAsync();
    }
    public virtual async Task<TResult?> GetSinglePropertyValue<TResult>(Func<TEntity, TResult> propertySelector,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null,
         Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false)
    {
        var query = dbSet.AsNoTracking().AsQueryable();

        if (isDeleteSelector != null)
            query = query.Where(entity => isDeleteSelector(entity) == notDeletedFlagValue);

        if (additionalQuery != null)
            query = additionalQuery(query);

        return await query.Select(entity => propertySelector(entity)).FirstOrDefaultAsync();
    }

    public virtual async Task<int> GetTableRecordsCount(Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null,
         Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false)
    {
        var query = dbSet.AsQueryable();

        if (isDeleteSelector != null)
            query = query.Where(entity => isDeleteSelector(entity) == notDeletedFlagValue);

        if (additionalQuery != null)
            query = additionalQuery(query);

        return await query.CountAsync();
    }

    public virtual async Task<bool> Add(TEntity entity)
    {
        if (entity == null)
            return false;

        await dbSet.AddAsync(entity);
        return await SaveChangesAsync() > 0 ? true : false;
    }

    public virtual async Task<bool> Update(Func<TEntity, TIdType> idSelector, TEntity entity,
        Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false)
    {
        if (entity == null)
            return false;

        TEntity? entityFromDb = await GetById(idSelector, idSelector(entity));

        if (entityFromDb == null)
            return false;

        typeof(TEntity).GetProperties()
        .Where(prop =>
            prop.CanRead &&
            prop.CanWrite &&
            prop.GetValue(entity) != null &&
            !prop.GetValue(entity)!.Equals(GetDefaultValue(prop.PropertyType)))
                                   .ToList()
                                   .ForEach(prop =>
                                   {
                                        var value = prop.GetValue(entity);
                                        prop.SetValue(entityFromDb, value);
                                   });

        PropertyInfo? isDeleteProperty = null;

        if (isDeleteSelector != null)
            isDeleteProperty = typeof(TEntity).GetProperties()
                                              .FirstOrDefault(prop => prop.PropertyType == typeof(bool) &&
                                                                       prop.Name == isDeleteSelector(entity).GetType().Name);
        if (isDeleteProperty != null)
            isDeleteProperty.SetValue(entity, notDeletedFlagValue);

        await Task.Run(() => dbSet.Update(entityFromDb));

        return await SaveChangesAsync() > 0 ? true : false;
    }

    public virtual async Task<bool> HardUpdateEntity(TEntity entity,
         Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false)
    {
        if (entity == null)
            return false;

        PropertyInfo? isDeleteProperty = null;

        if (isDeleteSelector != null)
            isDeleteProperty = typeof(TEntity).GetProperties()
                                              .FirstOrDefault(prop => prop.PropertyType == typeof(bool) &&
                                                                       prop.Name == isDeleteSelector(entity).GetType().Name);
        if (isDeleteProperty != null)
            isDeleteProperty.SetValue(entity, notDeletedFlagValue);

        await Task.Run(() => dbSet.Update(entity));

        return await SaveChangesAsync() > 0 ? true : false;
    }

    public virtual async Task<bool> Remove(Func<TEntity, TIdType> idSelector, TIdType id,
        Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false)
    {
        TEntity? entityFromDb = await GetById(idSelector, id);

        if (entityFromDb == null)
            return false;

        PropertyInfo? isDeleteProperty = null;

        if (isDeleteSelector != null)
            isDeleteProperty = typeof(TEntity).GetProperties()
                                              .FirstOrDefault(prop => prop.PropertyType == typeof(bool) &&
                                                                       prop.Name == isDeleteSelector(entityFromDb).GetType().Name);
        if (isDeleteProperty != null)
        {
            isDeleteProperty.SetValue(entityFromDb, notDeletedFlagValue);

            await Task.Run(() => dbSet.Update(entityFromDb));
        }
        else
            await Task.Run(() => dbSet.Remove(entityFromDb));

        return await SaveChangesAsync() > 0 ? true : false;
    }

    public async Task<bool> Remove(Func<TEntity, TIdType> idSelector, TEntity entity,
        Func<TEntity, bool>? isDeleteSelector = null, bool notDeletedFlagValue = false)
        => await Remove(idSelector, idSelector(entity), isDeleteSelector, notDeletedFlagValue);

    public async Task<IDbContextTransaction> GetTransaction() => await _context.Database.BeginTransactionAsync();
    protected async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    private object? GetDefaultValue(Type type)
    => type.IsValueType ? Activator.CreateInstance(type) : null;
}
