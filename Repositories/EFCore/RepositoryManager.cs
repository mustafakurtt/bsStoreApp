using Repositories.Contracts;

namespace Repositories.EFCore;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public IBookRepository Book => new BookRepository(_repositoryContext);
    public void Save()
    {
        _repositoryContext.SaveChanges();
    }
}