using Library_EF_App.Core.Domain;
using Library_EF_App.Core.IRepositories;
using System.Linq;
using System.Data.Entity;

namespace Library_EF_App.Persistence.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context)
        {
        }

        public Book GetBookWithAuthor(int id)
        {
            return LibraryContext.Books.Include(b => b.Author).SingleOrDefault(b => b.Id == id);
        }

        public LibraryContext LibraryContext
        {
            get { return Context as LibraryContext; }
        }
    }
}
