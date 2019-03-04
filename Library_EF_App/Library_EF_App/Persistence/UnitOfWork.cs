using Library_EF_App.Core;
using Library_EF_App.Persistence.Repositories;
using Library_EF_App.Core.IRepositories;

namespace Library_EF_App.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _context;

        public UnitOfWork(LibraryContext context)
        {
            _context = context;
            Authors = new AuthorRepository(_context);
            Books = new BookRepository(_context);
            Orders = new OrderRepository(_context);
            Users = new UserRepository(_context);
        }

        public IAuthorRepository Authors { get; private set; }
        public IBookRepository Books { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IUserRepository Users { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges(); // Vad returnerar den här? En int, men vad blir resultatet av _context.SaveChanges()? 1 eller 0 beroende på om något går fel?
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
