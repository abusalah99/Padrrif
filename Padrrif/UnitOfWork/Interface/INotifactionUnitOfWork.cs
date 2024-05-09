namespace Padrrif;

public interface INotifactionUnitOfWork : IUnitOfWork<Notifaction>
{
    Task<List<Notifaction>> GetUnSeenNotifactions();
}
