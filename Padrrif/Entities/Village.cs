namespace Padrrif;

public class Village : BaseEntity
{
    public string Name { get; set; } = null!;
    [JsonIgnore]
    public ICollection<Damage>? Damages { get; set; }
}
