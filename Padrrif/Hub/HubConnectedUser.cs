namespace Padrrif;

public record HubConnectedUser
{
    public Guid Id { get; set; }
    public string ConnectionId { get; set; } = null!;
    public RoleEnum Role { get; set; }
    public Guid GovernorateId { get; set; }
}

public class NotificationHubConecctedUsers
{
    public List<HubConnectedUser> HubConnectedUsers { get; set; } = new();
}
