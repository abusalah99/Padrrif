namespace Padrrif;

public interface IOwnerShipTypeUnitOfWork : IUnitOfWork<OwnerShipType>
{
    OwnerShipType MapFromOwnerShipTypeDtoToOwnerShipType(OwnerShipTypeDto dto);
}