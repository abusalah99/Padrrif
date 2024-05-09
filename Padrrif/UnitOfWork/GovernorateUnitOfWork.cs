
namespace Padrrif;

public class GovernorateUnitOfWork : UnitOfWork<Governorate>, IGovernorateUnitOfWork
{
    public GovernorateUnitOfWork(IRepository<Governorate> repository) : base(repository) { }

    public override Task Create(Governorate entity)
    {
        entity.Id = Guid.Empty;
        entity.IsDeleted = false;

        return base.Create(entity);
    }
    public Governorate MapFromGovernorateDtoToGovernorate(GovernorateDto dto)
        => new()
        {
            Id = dto.Id,
            Name = dto.Name
        };
}
