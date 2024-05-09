
namespace Padrrif;
public class OwnerShipTypeUnitOfWork : UnitOfWork<OwnerShipType>, IOwnerShipTypeUnitOfWork
{
    public OwnerShipTypeUnitOfWork(IRepository<OwnerShipType> repository) : base(repository) { }

    public override Task Create(OwnerShipType entity)
    {
        entity.Id = Guid.Empty;
        entity.IsDeleted = false;

        return base.Create(entity);
    }
    public OwnerShipType MapFromOwnerShipTypeDtoToOwnerShipType(OwnerShipTypeDto dto)
        => new()
        {
            Id = dto.Id,
            Name = dto.Name
        };
}