using Entities.Models;

namespace Repositories.Contracts;

public interface IBookRepository : IRepositoryBase<Book>
{
    Book? GetOneBookById(int id, bool trackChanges);
}