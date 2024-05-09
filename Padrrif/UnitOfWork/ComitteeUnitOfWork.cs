
namespace Padrrif;

public class ComitteeUnitOfWork : UnitOfWork<Comitee>, IComitteeUnitOfWork
{
    public ComitteeUnitOfWork(IRepository<Comitee> repository) : base(repository) { }

    public override Task Create(Comitee entity)
    {
        entity.Id = Guid.Empty;
        entity.IsDeleted = false;
        return base.Create(entity);
    }
    public Comitee MapFromComitteeDtoToComittee(ComiteeDto dto)
        => new()
        {
            Id = dto.Id,
            Name = dto.Name
        };
}