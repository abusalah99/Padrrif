namespace Padrrif;

public class AnimalDamage : BaseEntity
{
    public string? Type { get; set; }
    public string? DamageType { get; set; }
    public string? Insurance { get; set; }
    public int? TotalNumber { get; set; }
    public int? AffectedNumber { get; set; }
    public double? ProductionRate { get; set; }
    public double? PriceBeforeDamage { get; set; }
    public string? InsuranceProvider { get; set; }
    public Guid DamageId { get; set; }
    [JsonIgnore]
    public Damage Damage { get; set; } = null!;
}
