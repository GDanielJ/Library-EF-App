using Library_EF_App.Core.IRepositories;
using Library_EF_App.Core.Domain;
using System.Data.Entity;
using System.Linq;

namespace Library_EF_App.Persistence.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext context) : base(context) // Vad gör sista delen här? ( : base(context)). base låter mig få tillgång till constructorn i DbContext, istället för klassen Repository? Varför gör jag såhär? :) Eller är det Repository som är bas-klass?
        {
        }

        public Author GetAuthorWithBooks(int id)
        {
            return LibraryContext.Authors.Include(a => a.Books).SingleOrDefault(a => a.Id == id);
        }

        public LibraryContext LibraryContext // Vad är poängen med att göra så här? Den här upprepar jag i varje repository. Inte bra... går att lösa snyggare.
        {
            get { return Context as LibraryContext; }
        }
    }
}
