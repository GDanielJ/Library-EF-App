using Library_EF_App.Core.Domain;

namespace Library_EF_App.Core.IRepositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Author GetAuthorWithBooks(int id);
    }
}
