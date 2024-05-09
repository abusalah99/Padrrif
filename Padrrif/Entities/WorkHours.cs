namespace Padrrif;

public class WorkHours : BaseEntity
{
    public string? Name { get; set; }
    public string? Gender { get; set; }
    public double? DailyWorkHours { get; set; }
    public double? WeeklyWorkHours { get; set; }
    public Guid DamageId { get; set; }
    [JsonIgnore]
    public Damage Damage { get; set; } = null!;
}
