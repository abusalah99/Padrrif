namespace Padrrif;

public interface INotificationClient
{
    Task ReciveNotification(string message);
}
