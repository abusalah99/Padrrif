namespace Padrrif;

public interface IAuthUnitOfWork
{
    Task<TokenDto> RegisterAsFarmer(User user);
    Task RegisterAsEmpolyee(User user);
    Task<TokenDto?> Login(LoginDto loginDto);
    Task<User> MapFromUserRegistrationDtoToUser(UserRegistrationDto dto);
}
