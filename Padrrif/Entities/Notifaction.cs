namespace Padrrif;

public class Notifaction : BaseEntity
{
    public string Message { get; set; } = null!;
    public DateTime? SeenAt { get; set; }
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
    public Guid UserId { get; set; } 
}

