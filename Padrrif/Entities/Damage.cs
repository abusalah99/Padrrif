namespace Padrrif;

public class Damage : BaseEntity
{
    public string? DocumentNumber { get; set; }
    public Guid EducationLevelId {  get; set; }
    public Guid FarmerId { get; set; }
    public Guid? EmployeeId { get; set; }
    public Guid VillageId { get; set; }
    public Guid OwnershipTypeId { get; set; }
    public int DocumentId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int FamilyMembers { get; set; }
    public int ChildrenUnderEighteen { get; set; }
    public bool IsOtherProfession { get; set; }
    public double MonthlyIncomeFromAgriculture { get; set; }
    public double ReliancePercentage { get; set; }
    public string LandNumber { get; set; } = null!;
    public string BasinNumber { get; set; } = null!;
    public int RegionType { get; set; }
    public string? ContractorName { get; set; }
    public int? ContractorId { get; set; }
    public DateTime? StartDate { get; set; }
    public string? ContractDuration { get; set; }
    public DateTime? EventDate { get; set; }
    public string? EventDuration { get; set; }
    public string? EventDescription { get; set; }
    public double? TotalArea { get; set; }
    public double? DamageRateLastFiveYears { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public bool IsIsraeliArmyChecked { get; set; }
    public bool IsSeparationWallChecked { get; set; }
    public bool IsMilitaryZoneChecked { get; set; }
    public bool IsMarketClosedChecked { get; set; }
    public string? SettlementName { get; set; }
    public string? CompanyOrFactoryName { get; set; }
    public bool IsSnowChecked { get; set; }
    public bool IsStrongWindChecked { get; set; }
    public bool IsHailChecked { get; set; }
    public bool IsFloodChecked { get; set; }
    public bool IsDroughtChecked { get; set; }
    public bool IsFrostChecked { get; set; }
    public bool IsExtremeTemperatureDropChecked { get; set; }
    public bool IsExtremeTemperatureRiseChecked { get; set; }
    public string? OtherTabTwoText { get; set; }
    public bool IsAnimalTremblingChecked { get; set; }
    public bool IsBirdFluChecked { get; set; }
    public bool IsLocustChecked { get; set; }
    public bool IsHarmfulWeedsChecked { get; set; }
    public string? otherTabThreeText { get; set; }
    public List<string> LocationPaths { get; set; } = null!;
    public List<string> DocumentsPaths { get; set; } = null!;
    public virtual EducationLevel EducationLevel { get; set; } = null!;
    public virtual Village Village { get; set; } = null!;
    public virtual OwnerShipType OwnerShipType { get; set; } = null!;
    public virtual ICollection<AnimalDamage>? AnimalDamages { get; set; }
    public virtual ICollection<FarmFacilities>? FarmFacilities { get; set; }
    public virtual ICollection<WorkHours>? WorkHours { get; set; }
    public virtual ICollection<PlantDamage>? PlantDamages { get; set; }
    public virtual ICollection<FisheryDamage>? FisheryDamages { get; set; }

    [JsonIgnore]
    public User? Employee { get; set; }
}
