namespace Padrrif;

public class GovernorateUnitOfWork : UnitOfWork<Governorate>, IGovernorateUnitOfWork
{
    public GovernorateUnitOfWork(IRepository<Governorate, Guid> repository) : base(repository) { }

    public Governorate MapFromGovernorateDtoToGovernorate(GovernorateDto dto)
        => new()
        {
            Name = dto.Name
        };
}