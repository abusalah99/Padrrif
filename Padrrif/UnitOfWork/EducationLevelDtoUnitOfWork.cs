
namespace Padrrif;

public class EducationLevelDtoUnitOfWork : UnitOfWork<EducationLevel>, IEducationLevelDtoUnitOfWork
{
    public EducationLevelDtoUnitOfWork(IRepository<EducationLevel> repository) : base(repository) { }

    public override Task Create(EducationLevel entity)
    {
        entity.Id = Guid.Empty;
        entity.IsDeleted = false;

        return base.Create(entity);
    }
    public EducationLevel MapFromEducationLevelDtoToEducationLevel(EducationLevelDto dto)
        => new()
        {
            Id = dto.Id,
            Name = dto.Name
        };
}
