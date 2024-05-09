namespace Padrrif;

public class UserRegistrationDto 
{
    public string Name { get; set; } = null!;
    public int IdentityNumber { get; set; }
    public Guid GovernorateId { get; set; }
    public string? City { get; set; }
    public SexEnum Sex { get; set; }
    public string MobilePhoneNumber { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string Password { get; set; } = null!;
    public IFormFile Image { get; set; } = null!;
    public List<IFormFile> Documents { get; set; } = null!;
    public Guid? CommiteeId { get; set; }
}