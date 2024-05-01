namespace Padrrif;
public class AuthUnitOfWork : IAuthUnitOfWork
{
    private readonly IRepository<User, Guid> _repository;
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IJwtProvider _jwtProvider;
    private readonly JwtAccessOptions _jwtAccessOptions;

    public AuthUnitOfWork(IRepository<User, Guid> repository, IWebHostEnvironment env,
        IHttpContextAccessor contextAccessor, IJwtProvider jwtProvider, IOptions<JwtAccessOptions> jwtAccessOption)
    {
        _repository = repository;
        _env = env;
        _contextAccessor = contextAccessor;
        _jwtProvider = jwtProvider;
        _jwtAccessOptions = jwtAccessOption.Value;
    }

    public async Task<TokenDto> RegisterAsFarmer(User user) => await Register(user, RoleEnum.Farmer);
    public async Task<TokenDto> RegisterAsEmpolyee(User user) => await Register(user, RoleEnum.Empolyee);

    public async Task<TokenDto?> Login(LoginDto dto)
    {
        User? userFromDb = null;

        if (dto.IdentityNumber > 99999999)
            userFromDb = await _repository.GetSingleEntityWithSomeCondiition(q => q.Where(u => u.IdentityNumber == dto.IdentityNumber && u.IsConfirmed),
                                                                             u => u.IsDeleted, false);

        if (!dto.Email.IsNullOrEmpty())
            userFromDb = await _repository.GetSingleEntityWithSomeCondiition(q => q.Where(u => u.Email == dto.Email && u.IsConfirmed), u => u.IsDeleted, false);

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
        };

        string? imageName = null;

        if (dto.Image != null)
            imageName = await dto.Image.SaveImageAsync(_env);

        user.ImagePath = imageName.GetFileUrl(_contextAccessor) ?? "";

        return user;
    }

    private async Task<TokenDto> Register(User user, RoleEnum role)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        if (role == RoleEnum.Farmer)
        {
            user.Role = RoleEnum.Farmer;
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
