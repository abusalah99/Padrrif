namespace Padrrif;

public class Governorate : BaseEntity
{
    public string Name { get; set; } = null!;
    public virtual ICollection<User>? Users { get; set; }
}
