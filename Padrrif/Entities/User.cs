namespace Padrrif;
public class User : BaseEntity
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
    [JsonIgnore]
    public string Password { get; set; } = null!;
    public RoleEnum Role { get; set; } = RoleEnum.Farmer;
    public string ImagePath { get; set; } = null!;
    public List<string> DocumentsPaths { get; set; } = null!;
    [JsonIgnore]
    public bool IsConfirmed { get; set; } = false;
    public virtual Governorate Governorate { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Notifaction>? Notifactions { get; set; }
    
    public virtual Comitee? Comittee { get; set; }
    public Guid? ComiteeId { get; set; }
    [JsonIgnore]
    public ICollection<Damage>? Damages { get; set; }
}

