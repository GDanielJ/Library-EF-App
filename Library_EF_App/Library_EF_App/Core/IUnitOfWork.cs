using System;
using Library_EF_App.Core.IRepositories;

namespace Library_EF_App.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Authors { get;  }
        IBookRepository Books { get; }
        IOrderRepository Orders { get; }
        IUserRepository Users { get; }

        int Complete();
    }
}
