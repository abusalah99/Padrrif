namespace Padrrif;

public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;
    public UnitOfWork(IRepository<TEntity> repository) => _repository = repository;


    public virtual async Task<List<TEntity>> Read(Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null) =>
        await ExcuteMethod((_) => _repository.GetList(additionalQuery), skipTransaction: true) as List<TEntity> ?? new();

    public virtual async Task<TEntity?> Read(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalQuery = null) =>
         await ExcuteMethod((_) => _repository.GetById(id), skipTransaction: true) as TEntity;

    public virtual async Task Create(TEntity entity) => await ExcuteMethod((arg) => _repository.Add(arg), entity);

    public virtual async Task Update(TEntity entity) => await ExcuteMethod((arg) => _repository.Update(arg), entity);

    private async Task<dynamic> ExcuteMethod(Func<dynamic?, dynamic> method, dynamic? arg = null, bool skipTransaction = false)
    {
        if (skipTransaction)
            try
            {
                return await method.Invoke(arg);
            }
            catch (Exception ex)
            {
                return false;
            }

        using IDbContextTransaction transaction = await _repository.GetTransaction();

        try
        {
            var result = await method.Invoke(arg);

            await transaction.CommitAsync();

            return result;
        }
        catch
        {
            transaction.Rollback();

            return false;
        }
    }
}