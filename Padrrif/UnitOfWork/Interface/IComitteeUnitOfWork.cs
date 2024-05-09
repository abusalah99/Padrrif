namespace Padrrif;

public interface IComitteeUnitOfWork : IUnitOfWork<Comitee>
{
    Comitee MapFromComitteeDtoToComittee(ComiteeDto dto);
}
