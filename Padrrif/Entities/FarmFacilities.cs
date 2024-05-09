namespace Padrrif;

public class FarmFacilities : BaseEntity
{
    public string? FacilitiesType { get; set; }
    public string? DamageFacilitiesType { get; set; }
    public string? CategoryFacilities { get; set; }
    public string? LicenseFacilities { get; set; }
    public string? InsuranceFacilities { get; set; }
    public double? TotalAffectedArea { get; set; }
    public double? ActualDamageArea { get; set; }
    public string? LicenseProvider { get; set; }
    public string? InsuranceProvider { get; set; }
    public Guid DamageId { get; set; }
    [JsonIgnore]
    public Damage Damage { get; set; } = null!;
}
