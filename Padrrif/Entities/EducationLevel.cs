namespace Padrrif;

public class EducationLevel : BaseEntity
{
    public string Name { get; set; } = null!;
    [JsonIgnore]
    public ICollection<Damage>? Damages { get; set; }
 
}
