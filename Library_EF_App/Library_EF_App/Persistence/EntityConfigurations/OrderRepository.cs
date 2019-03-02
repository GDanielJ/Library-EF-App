using Library_EF_App.Persistence.Repositories;
using Library_EF_App.Core.Domain;
using Library_EF_App.Core.IRepositories;
using System.Linq;
using System.Data.Entity;

namespace Library_EF_App.Persistence.EntityConfigurations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(LibraryContext context) : base(context)
        {
        }

        public Order GetOrderWithUser(int id)
        {
            return LibraryContext.Orders.Include(o => o.User).SingleOrDefault(o => o.Id == id);
        }

        public LibraryContext LibraryContext
        {
            get { return Context as LibraryContext; }
        }
    }
}
