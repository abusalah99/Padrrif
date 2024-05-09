namespace Padrrif;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _context;
    protected DbSet<TEntity> dbSet;
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<List<TEntity>> GetList(Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null)
    {
        IQueryable<TEntity> query = dbSet.AsNoTracking().AsQueryable();

        if (additionalQuery != null)
            query = additionalQuery(query);

        var entities = await query.Where(e => e.IsDeleted == false).ToListAsync();

        return entities;
    }
    public virtual async Task<TEntity?> GetById(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null)
    {
        IQueryable<TEntity> query = dbSet.AsNoTracking();

        if (additionalQuery != null)
            query = additionalQuery(query);

        return await query.FirstOrDefaultAsync(e => e.IsDeleted == false && e.Id == id);

    }
    public virtual async Task<TEntity?> GetSingleEntityWithSomeCondiition(Func<IQueryable<TEntity>, IQueryable<TEntity>> additionalQuery)

    {
        IQueryable<TEntity> query = dbSet.Where(e => e.IsDeleted == false).AsNoTracking();

        query = additionalQuery(query);

        return await query.FirstOrDefaultAsync();
    }
    

    public virtual async Task<List<TResult>> SelectListOfProperty<TResult>(Func<TEntity, TResult> propertySelector,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null)
    {
        IQueryable<TEntity> query = dbSet.Where(e => e.IsDeleted == false).AsNoTracking();

        if (additionalQuery != null)
            query = additionalQuery(query);

        return await query.Select(entity => propertySelector(entity)).ToListAsync();
    }
    public virtual async Task<TResult?> GetSinglePropertyValue<TResult>(Func<TEntity, TResult> propertySelector,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null)
    {
        IQueryable<TEntity> query = dbSet.Where(e => e.IsDeleted == false).AsNoTracking();

        if (additionalQuery != null)
            query = additionalQuery(query);

        return await query.Select(entity => propertySelector(entity)).FirstOrDefaultAsync();
    }

    public virtual async Task<int> GetTableRecordsCount(Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null)
    {
        IQueryable<TEntity> query = dbSet.Where(e => e.IsDeleted == false).AsQueryable();

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

    public virtual async Task<bool> Update(TEntity entity)
    {
        if (entity == null)
            return false;

        TEntity? entityFromDb = await GetById(entity.Id);

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

        await Task.Run(() => dbSet.Update(entityFromDb));

        return await SaveChangesAsync() > 0 ? true : false;
    }

    public virtual async Task<bool> HardUpdateEntity(TEntity entity)
    {
        if (entity == null)
            return false;

        TEntity? entityFromDb = await GetById(entity.Id);

        if (entityFromDb == null)
            return false;

        await Task.Run(() => dbSet.Update(entity));

        return await SaveChangesAsync() > 0 ? true : false;
    }

    public virtual async Task<bool> Remove( Guid id)
    {
        TEntity? entityFromDb = await GetById(id);

        if (entityFromDb == null)
            return false;

        entityFromDb.IsDeleted = true;

        await Task.Run(() => Update(entityFromDb));

        return await SaveChangesAsync() > 0 ? true : false;
    }

    public async Task<bool> Remove(TEntity entity)
        => await Remove(entity.Id);

    public async Task<IDbContextTransaction> GetTransaction() => await _context.Database.BeginTransactionAsync();
    protected async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    private object? GetDefaultValue(Type type)
    => type.IsValueType ? Activator.CreateInstance(type) : null;
}
