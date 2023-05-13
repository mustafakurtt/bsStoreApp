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

    public IEnumerable<Book> GetAllBooks(bool trackChanges)
    {
        _logger.LogInfo( "Services.IBookService.GetAllBooks function called");
        return _manager.Book.GetAllBooks(trackChanges);
    }

    public Book GetOneBookById(int id, bool trackChanges)
    {
        var book = _manager.Book.GetOneBookById(id, trackChanges);
        if (book is null) throw new BookNotFoundException(id);
        return book;
    }

    public void CreateOneBook(Book book)
    {
        if (book is null) throw new ArgumentNullException(nameof(book));
        _manager.Book.CreateOneBook(book);
        _manager.Save();
    }

    public void UpdateOneBook(int id,BookDtoForUpdate bookDto)
    {
        if (bookDto is null) throw new ArgumentNullException(nameof(bookDto));
        var entity = _manager.Book.GetOneBookById(id,true);
        if (entity is null)
        {
            string message = $"The book with id:{id} could not be found";
            _logger.LogInfo(message);
            throw new Exception(message);
        }

        entity = _mapper.Map<Book>(bookDto);
        _manager.Book.UpdateOneBook(entity);
        _manager.Save();
    }

    public void DeleteOneBook(int id)
    {
        var entity = _manager.Book.GetOneBookById(id, true);
        if (entity is null)
        {
            string message = $"The book with id:{id} could not be found";
            _logger.LogInfo(message);
            throw new Exception(message);
        }

        _manager.Book.DeleteOneBook(entity);
        _manager.Save();
    }

    public Book PartiallyUpdateOneBook(int id, JsonPatchDocument<Book> bookPatch)
    {
        if (bookPatch is null) throw new ArgumentNullException(nameof(bookPatch));
        var entity = _manager.Book.GetOneBookById(id,true);
        if (entity is null) throw new Exception($"Book with id:{id} could not be found");
        bookPatch.ApplyTo(entity);
        _manager.Book.UpdateOneBook(entity);
        _manager.Save();
        return entity;
    }

}