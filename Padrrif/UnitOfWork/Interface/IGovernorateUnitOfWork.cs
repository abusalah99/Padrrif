namespace Padrrif;

public interface IGovernorateUnitOfWork : IUnitOfWork<Governorate>
{
    Governorate MapFromGovernorateDtoToGovernorate(GovernorateDto dto);
}
