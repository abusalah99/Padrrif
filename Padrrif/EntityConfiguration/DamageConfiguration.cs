namespace Padrrif;

public class DamageConfiguration : BaseEntityConfiguration<Damage>
{
    public override void Configure(EntityTypeBuilder<Damage> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.DocumentId).ValueGeneratedOnAdd().IsRequired();

        builder.HasIndex(e => e.DocumentId).IsUnique();

        builder.HasMany(e => e.AnimalDamages).WithOne(e => e.Damage).HasForeignKey(e => e.DamageId);
        builder.HasMany(e => e.FarmFacilities).WithOne(e => e.Damage).HasForeignKey(e => e.DamageId);
        builder.HasMany(e => e.WorkHours).WithOne(e => e.Damage).HasForeignKey(e => e.DamageId);
        builder.HasMany(e => e.PlantDamages).WithOne(e => e.Damage).HasForeignKey(e => e.DamageId);
        builder.HasMany(e => e.FisheryDamages).WithOne(e => e.Damage).HasForeignKey(e => e.DamageId);
        builder.HasOne(e => e.Employee).WithMany(e => e.Damages).HasForeignKey(e => e.EmployeeId).IsRequired(false);
    }
}
