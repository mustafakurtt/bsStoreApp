using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class BookManager : IBookService
{
    private readonly IRepositoryManager _manager;
    private readonly ILoggerService _logger;

    public BookManager(IRepositoryManager manager, ILoggerService logger)
    {
        _manager = manager;
        _logger = logger;
    }

    public IEnumerable<Book> GetAllBooks(bool trackChanges)
    {
        _logger.LogInfo( "Services.IBookService.GetAllBooks function called");
        return _manager.Book.GetAllBooks(trackChanges);
    }

    public Book? GetOneBookById(int id, bool trackChanges)
    {
        return _manager.Book.GetOneBookById(id, trackChanges);
    }

    public void CreateOneBook(Book book)
    {
        if (book is null) throw new ArgumentNullException(nameof(book));
        _manager.Book.CreateOneBook(book);
        _manager.Save();
    }

    public void UpdateOneBook(Book book)
    {
        if (book is null) throw new ArgumentNullException(nameof(book));
        int id = book.Id;
        var entity = _manager.Book.GetOneBookById(id,true);
        if (entity is null)
        {
            string message = $"The book with id:{id} could not be found";
            _logger.LogInfo(message);
            throw new Exception(message);
        }
        entity.Title = book.Title;
        entity.Price = book.Price;
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