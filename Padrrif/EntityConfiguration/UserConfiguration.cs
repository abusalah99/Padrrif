namespace Padrrif;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(e => e.IdentityNumber)
               .HasMaxLength(9)
               .IsRequired();

        builder.Property(e => e.City)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(e => e.MobilePhoneNumber)
               .HasMaxLength(15)
               .IsRequired();

        builder.Property(e => e.MobilePhoneNumber)
                .HasMaxLength(15)
                .IsRequired();

        builder.HasIndex(e => e.Email)
               .IsUnique();
        builder.Property(e => e.Email)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(e => e.Password)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(e => e.ImagePath)
               .IsRequired();

        builder.HasMany(e => e.Notifactions)
               .WithOne(e => e.User)
               .HasForeignKey(e => e.UserId);
    }
}
