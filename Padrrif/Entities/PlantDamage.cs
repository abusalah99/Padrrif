namespace Padrrif;

public class PlantDamage : BaseEntity
{
    public string? CropType { get; set; }
    public string? CropDamage { get; set; }
    public string? CropAge { get; set; }
    public string? IrrigationMethod { get; set; }
    public string? IrrigationOption { get; set; }
    public string? OtherTextFieldValue { get; set; }
    public string? CropTypeMethod { get; set; }
    public string? CropTypeOption { get; set; }
    public string? OtherCropTextFieldValue { get; set; }
    public double? TotalAffectedArea { get; set; }
    public double? ActualDamageArea { get; set; }
    public double? PercentageDamage { get; set; }
    public double? EstimatedProductionRate { get; set; }
    public double? EstimatedPrice { get; set; }
    public string? FruitingStage { get; set; }
    public string? Insurance { get; set; }
    public string? InsuranceProvider { get; set; }
    public Guid DamageId { get; set; }
    [JsonIgnore]
    public Damage Damage { get; set; } = null!;
}
