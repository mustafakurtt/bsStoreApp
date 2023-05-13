using AutoMapper;
using Entities.Dtos;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class BookManager : IBookService
{
    private readonly IRepositoryManager _manager;
    private readonly ILoggerService _logger;
    private readonly IMapper _mapper;

    public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
    {
        _manager = manager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges)
    {
        _logger.LogInfo( "Services.IBookService.GetAllBooks function called");
        var books = _manager.Book.GetAllBooksAsync(trackChanges);
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges)
    {
        var book = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
        if (book is null) throw new BookNotFoundException(id);
        return book;
    }

    public async Task<BookDto> CreateOneBookAsync(Book book)
    {
        if (book is null) throw new ArgumentNullException(nameof(book));
        var entity = _mapper.Map<Book>(book);
        _manager.Book.CreateOneBook(entity);
        await _manager.SaveAsync();
        return _mapper.Map<BookDto>(entity);
    }

    public async Task UpdateOneBookAsync(int id,BookDtoForUpdate bookDto)
    {
        if (bookDto is null) throw new ArgumentNullException(nameof(bookDto));
        var entity = await _manager.Book.GetOneBookByIdAsync(id, true);
        if (entity is null)
        {
            string message = $"The book with id:{id} could not be found";
            _logger.LogInfo(message);
            throw new Exception(message);
        }

        entity = _mapper.Map<Book>(bookDto);
        _manager.Book.UpdateOneBook(entity);
        await _manager.SaveAsync();
    }

    public async Task DeleteOneBookAsync(int id)
    {
        var entity = await _manager.Book.GetOneBookByIdAsync(id, true);
        if (entity is null)
        {
            string message = $"The book with id:{id} could not be found";
            _logger.LogInfo(message);
            throw new Exception(message);
        }

        _manager.Book.DeleteOneBook(entity);
        await _manager.SaveAsync();
    }

    public async Task<Book> PartiallyUpdateOneBookAsync(int id, JsonPatchDocument<Book> bookPatch)
    {
        if (bookPatch is null) throw new ArgumentNullException(nameof(bookPatch));
        var entity = await _manager.Book.GetOneBookByIdAsync(id,true);
        if (entity is null) throw new Exception($"Book with id:{id} could not be found");
        bookPatch.ApplyTo(entity);
        _manager.Book.UpdateOneBook(entity); 
        await _manager.SaveAsync();
        return entity ;
    }

}