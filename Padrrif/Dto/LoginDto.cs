namespace Padrrif;

public class LoginDto
{
    public int IdentityNumber { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; } = null!;
}
