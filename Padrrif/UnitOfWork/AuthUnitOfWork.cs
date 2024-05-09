namespace Padrrif;
public class AuthUnitOfWork : IAuthUnitOfWork
{
    private readonly IRepository<User> _repository;
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IJwtProvider _jwtProvider;
    private readonly JwtAccessOptions _jwtAccessOptions;
    private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;
    private readonly NotificationHubConecctedUsers _conecctedUsers;
    private readonly IRepository<Notifaction> _notifactionRepository;

    public AuthUnitOfWork(IRepository<User> repository, IWebHostEnvironment env,
        IHttpContextAccessor contextAccessor, IJwtProvider jwtProvider, IOptions<JwtAccessOptions> jwtAccessOption,
        IHubContext<NotificationHub, INotificationClient> hubContext, NotificationHubConecctedUsers conecctedUsers, IRepository<Notifaction> notifactionRepository)
    {
        _repository = repository;
        _env = env;
        _contextAccessor = contextAccessor;
        _jwtProvider = jwtProvider;
        _jwtAccessOptions = jwtAccessOption.Value;
        _hubContext = hubContext;
        _conecctedUsers = conecctedUsers;
        _notifactionRepository = notifactionRepository;
    }

    public async Task<TokenDto> RegisterAsFarmer(User user) => await Register(user, RoleEnum.Farmer);
    public async Task RegisterAsEmpolyee(User user) => await Register(user, RoleEnum.Empolyee);

    public async Task<TokenDto?> Login(LoginDto dto)
    {
        User? userFromDb = null;

        if (dto.IdentityNumber > 99999999)
            userFromDb = await _repository.GetSingleEntityWithSomeCondiition(q => q.Where(u => u.IdentityNumber == dto.IdentityNumber && u.IsConfirmed));

        if (!dto.Email.IsNullOrEmpty())
            userFromDb = await _repository.GetSingleEntityWithSomeCondiition(q => q.Where(u => u.Email == dto.Email && u.IsConfirmed));

        if (userFromDb == null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, userFromDb.Password))
            return null;

        return new()
        {
            Value = _jwtProvider.GenrateAccessToken(userFromDb),
            ExpireAt = DateTime.UtcNow.AddMonths(_jwtAccessOptions.ExpireTimeInMonths),
        };
    }
    public async Task<User> MapFromUserRegistrationDtoToUser(UserRegistrationDto dto)
    {
        User user = new()
        {
            Email = dto.Email,
            Name = dto.Name,
            IdentityNumber = dto.IdentityNumber,
            GovernorateId = dto.GovernorateId,
            City = dto.City,
            Sex = dto.Sex,
            MobilePhoneNumber = dto.MobilePhoneNumber,
            PhoneNumber = dto.PhoneNumber,
            BirthDate = dto.BirthDate,
            Password = dto.Password,
            ComiteeId = dto.CommiteeId
        };

        string? imageName = null;

        if (dto.Image != null)
            imageName = await dto.Image.SaveImageAsync(_env);

        user.ImagePath = imageName.GetFileUrl(_contextAccessor) ?? "";
        List<string>? documentsPaths = null;
        if(dto.Documents != null && dto.Documents.Any())
        {
            documentsPaths = new();
            foreach (var document in dto.Documents)
            {
                string path = await document.SaveImageAsync(_env);
                documentsPaths.Add(path.GetFileUrl(_contextAccessor) ?? "");
            }
            user.DocumentsPaths = documentsPaths;
        } 

        return user;
    }

    private async Task<TokenDto> Register(User user, RoleEnum role)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        if (role == RoleEnum.Farmer)
        {
            user.Role = RoleEnum.Farmer;

            user.ComiteeId = null;

            List<User> employees = await _repository.GetList(q => q.Where(e => e.Role == RoleEnum.Empolyee && e.GovernorateId == user.GovernorateId));

            List<HubConnectedUser> onlineUsers = _conecctedUsers.HubConnectedUsers.Where(e => e.Role == RoleEnum.Empolyee && e.GovernorateId == user.GovernorateId)
                                                                      .ToList();

            List<Guid> onlineUsersIds = onlineUsers.Select(e => e.Id).ToList();

            List<Guid> offlineEmployeesIds = employees.Where(e => !onlineUsersIds.Contains(e.Id))
                                                   .Select(e=>e.Id)
                                                   .ToList();

            List<string> userHubIds = onlineUsers.Select(e => e.ConnectionId).ToList();

            
            string notifactionMessage = $"المزراع {user.Name} انشاء حساب و بانتظار الموافقة";

            foreach (var id in userHubIds)
                await _hubContext.Clients.Client(id).ReciveNotification(notifactionMessage);

            foreach (var id in offlineEmployeesIds)
            {
                Notifaction notifaction = new()
                {
                    Message = notifactionMessage,
                    UserId = id,
                    SeenAt = null
                };
                await _notifactionRepository.Add(notifaction);
            }
        }
        else
        {
            user.Role = RoleEnum.Empolyee;
            user.IsConfirmed = true;
        }

        await _repository.Add(user);

        return new()
        {
            Value = _jwtProvider.GenrateAccessToken(user),
            ExpireAt = DateTime.UtcNow.AddMonths(_jwtAccessOptions.ExpireTimeInMonths),
        };
    }
}
