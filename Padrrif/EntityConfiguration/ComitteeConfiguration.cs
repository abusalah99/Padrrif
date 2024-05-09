namespace Padrrif;

public class ComitteeConfiguration : BaseEntityConfiguration<Comitee>
{
    public override void Configure(EntityTypeBuilder<Comitee> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
               .HasMaxLength(50)
               .IsRequired();

        builder.HasMany(e => e.Employees).WithOne(e => e.Comittee).HasForeignKey(e => e.ComiteeId).IsRequired(false);
    }
}
