namespace Padrrif;
public class UserUnitOfWork : UnitOfWork<User>, IUserUnitOfWork
{
    public UserUnitOfWork(IRepository<User> repository) : base(repository){ }

    public async Task<List<User>> GetAllUnConfirmedUsers()
    {
        List<User> users = await Read(q => q.Where(e => e.IsConfirmed == false));
        return users;
    }

    public async Task<bool> ConfirmUser(Guid userId)
    {
        User? userFromDb = await Read(userId);

        if (userFromDb == null)
            return false;

        userFromDb.IsConfirmed = true;

        try
        {
            await Update(userFromDb);
            return true;
        }
        catch
        {
            return false;
        }
    }
}