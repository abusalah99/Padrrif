using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Padrrif;

public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : BaseEntity 
{
    private readonly IRepository<TEntity, Guid> _repository;
    public UnitOfWork(IRepository<TEntity, Guid> repository) => _repository = repository;


    public virtual async Task<List<TEntity>> Read() =>
        await ExcuteMethod((_) => _repository.GetList(isDeleteSelector: e => e.IsDeleted,
            notDeletedFlagValue: false), skipTransaction: true) as List<TEntity> ?? new();

    public virtual async Task<TEntity?> Read(Guid id) =>
         await ExcuteMethod((_) => _repository.GetById(e => e.Id, id, isDeleteSelector: e => e.IsDeleted,
            notDeletedFlagValue: false), skipTransaction: true) as TEntity;
 
    public virtual async Task Create(TEntity entity) => await ExcuteMethod((arg) => _repository.Add(arg), entity);

    public async Task<dynamic> ExcuteMethod(Func<dynamic?, dynamic> method, dynamic? arg = null, bool skipTransaction = false)
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

    protected Guid GetUserIdFromClaims(IHttpContextAccessor context, string clamiName)
    {
        var httpContext = context.HttpContext;

        if (httpContext == null)
            throw new InvalidOperationException("This operation requires an active HTTP context.");

        var claimsId = httpContext.User.FindFirst(@$"{clamiName}") ?? new("Id", Guid.Empty.ToString());

        return new(claimsId.Value);
    }
}