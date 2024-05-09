namespace Padrrif;

public class NotifactionUnitOfWork : UnitOfWork<Notifaction>, INotifactionUnitOfWork
{
    private readonly IHttpContextAccessor _contextAccessor;

    public NotifactionUnitOfWork(IRepository<Notifaction> repository, IHttpContextAccessor contextAccessor) : base(repository) => _contextAccessor = contextAccessor;

    public async Task<List<Notifaction>> GetUnSeenNotifactions()
    {
        Guid userId = _contextAccessor.GetUserId();

        List<Notifaction> notifactions =  await Read(q => q.Where(e => e.UserId == userId && e.SeenAt == null));

        foreach (var notifaction in notifactions)
        {
            notifaction.SeenAt = DateTime.UtcNow;

            await(Update(notifaction));
        }

        return notifactions;
    }
}