namespace Padrrif;

public class OwnerShipTypeConfiguration : BaseEntityConfiguration<OwnerShipType>
{
    public override void Configure(EntityTypeBuilder<OwnerShipType> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
               .HasMaxLength(50)
               .IsRequired();

        builder.HasMany(e => e.Damages).WithOne(e => e.OwnerShipType).HasForeignKey(e => e.OwnershipTypeId);
    }
}