namespace Padrrif;

public interface IUserUnitOfWork : IUnitOfWork<User>
{
    Task<List<User>> GetAllUnConfirmedUsers();
    Task<bool> ConfirmUser(Guid userId);
}
