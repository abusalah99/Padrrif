namespace Padrrif;

public class EducationLevelConfiguration : BaseEntityConfiguration<EducationLevel>
{
    public override void Configure(EntityTypeBuilder<EducationLevel> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
               .HasMaxLength(50)
               .IsRequired();

        builder.HasMany(e => e.Damages).WithOne(e => e.EducationLevel).HasForeignKey(e => e.EducationLevelId);
    }
}
