
namespace Padrrif;

public class VillageUnitOfWork : UnitOfWork<Village>, IVillageUnitOfWork
{
    public VillageUnitOfWork(IRepository<Village> repository) : base(repository) { }

    public override Task Create(Village entity)
    {
        entity.Id = Guid.Empty;
        entity.IsDeleted = false;

        return base.Create(entity);
    }
    public Village MapFromVillageDtoToVillage(VillageDto dto)
        => new()
        {
            Id = dto.Id,
            Name = dto.Name
        };
}