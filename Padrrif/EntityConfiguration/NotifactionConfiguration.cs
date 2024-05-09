namespace Padrrif;

public class NotifactionConfiguration : BaseEntityConfiguration<Notifaction>
{
    public override void Configure(EntityTypeBuilder<Notifaction> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Message)
               .HasMaxLength(150)
               .IsRequired();
    }
}
