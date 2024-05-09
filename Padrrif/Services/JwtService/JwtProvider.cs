namespace Padrrif;

public class JwtProvider : IJwtProvider
{
    private readonly JwtAccessOptions _jwtAccessOptions;

    public JwtProvider(IOptions<JwtAccessOptions> jwtAccessOption) => _jwtAccessOptions = jwtAccessOption.Value;
    public string GenrateAccessToken(User user)
    {
        var claims = new List<Claim>()
        {
            new("Id", user.Id.ToString()),
            new(JwtRegisteredClaimNames.Name, $"{user.Name}"),
            new(ClaimTypes.Role, user.Role.ToString()),
            new("GovernorateId", user.GovernorateId.ToString())
        };
 
       string token = TokenGenrator(
            _jwtAccessOptions.SecretKey,
            claims,
            null,
            null,
            DateTime.UtcNow.AddMonths(_jwtAccessOptions.ExpireTimeInMonths));

        return token;
    }

    private string TokenGenrator(
        string secretKey,
        List<Claim>? claims,
        String? issuser,
        String? audience,
        DateTime expireDate)
    {
        var SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
           issuser,
           audience,
           claims,
           null,
           expireDate,
           SigningCredentials);

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }

}
