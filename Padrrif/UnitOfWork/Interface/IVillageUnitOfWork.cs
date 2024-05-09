namespace Padrrif;

public interface IVillageUnitOfWork : IUnitOfWork<Village>
{
    Village MapFromVillageDtoToVillage(VillageDto dto);
}
