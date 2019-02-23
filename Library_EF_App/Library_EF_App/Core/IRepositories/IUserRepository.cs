using Library_EF_App.Core.Domain;

namespace Library_EF_App.Core.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserWithOrders(int id);
    }
}
