namespace Padrrif;

[Authorize]
public class NotificationHub : Hub<INotificationClient>
{
    private readonly NotificationHubConecctedUsers _conecctedUsers;

    public NotificationHub(NotificationHubConecctedUsers conecctedUsers) => _conecctedUsers = conecctedUsers;

    public override async Task OnConnectedAsync()
    {
        string userId = Context?.User?.FindFirstValue("Id") ?? "";
        string role = Context?.User?.FindFirstValue(ClaimTypes.Role) ?? "";
        string governorateId = Context?.User?.FindFirstValue("GovernorateId") ?? "";

        Guid userIdGuid = Guid.Parse(userId);

        if (!_conecctedUsers.HubConnectedUsers.Any(u => u.Id == userIdGuid))
        {
            var connectedUser = new HubConnectedUser
            {
                Id = userIdGuid,
                ConnectionId = Context!.ConnectionId,
                Role = (RoleEnum)Enum.Parse(typeof(RoleEnum), role),
                GovernorateId = Guid.Parse(governorateId)
            };

            _conecctedUsers.HubConnectedUsers.Add(connectedUser);
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var user = _conecctedUsers.HubConnectedUsers.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId);
        if (user != null)
        {
            _conecctedUsers.HubConnectedUsers.Remove(user);
        }
        await base.OnDisconnectedAsync(exception);
    }

}
