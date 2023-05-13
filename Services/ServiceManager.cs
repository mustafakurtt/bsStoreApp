using AutoMapper;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
    public readonly Lazy<IBookService> _bookService;
    public ServiceManager(IRepositoryManager repositoryManager,ILoggerService loggerService,IMapper mapper)
    {
        _bookService = new Lazy<IBookService>( () => new BookManager(repositoryManager,loggerService,mapper));
    }


    public IBookService BookService => _bookService.Value;
}