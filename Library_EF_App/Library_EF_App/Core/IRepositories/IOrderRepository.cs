using Library_EF_App.Core.Domain;

namespace Library_EF_App.Core.IRepositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetOrderWithUser(int id);
    }
}
