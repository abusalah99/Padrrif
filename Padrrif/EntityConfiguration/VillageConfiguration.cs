namespace Padrrif;

public class VillageConfiguration : BaseEntityConfiguration<Village>
{
    public override void Configure(EntityTypeBuilder<Village> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
               .HasMaxLength(50)
               .IsRequired();

        builder.HasMany(e => e.Damages).WithOne(e => e.Village).HasForeignKey(e => e.VillageId);
    }
}
