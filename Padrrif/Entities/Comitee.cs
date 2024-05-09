namespace Padrrif;

public class Comitee : BaseEntity
{
    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<User>? Employees { get; set; }
}
