namespace Padrrif;

public interface IJwtProvider
{
    string GenrateAccessToken(User user);

}
