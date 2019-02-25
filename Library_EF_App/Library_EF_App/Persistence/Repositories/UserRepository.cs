using Library_EF_App.Core.IRepositories;
using Library_EF_App.Core.Domain;
using System.Data.Entity;
using System.Linq;

namespace Library_EF_App.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(LibraryContext context) : base(context)
        {
        }

        public User GetUserWithOrders(int id)
        {
            return LibraryContext.Users.Include(u => u.Orders).SingleOrDefault(u => u.Id == id);
        }

        public LibraryContext LibraryContext
        {
            get { return Context as LibraryContext; }
        }
    }
}
