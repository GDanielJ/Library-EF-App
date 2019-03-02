using Library_EF_App.Core.Domain;

namespace Library_EF_App.Core.IRepositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Book GetBookWithAuthor(int id);
    }
}
