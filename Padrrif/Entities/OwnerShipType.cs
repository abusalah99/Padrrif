namespace Padrrif;

public class OwnerShipType : BaseEntity
{
    public string Name { get; set; } = null!;
    [JsonIgnore]
    public ICollection<Damage>? Damages { get; set; }
}  
