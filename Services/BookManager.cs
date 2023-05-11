using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class BookManager : IBookService
{
    private readonly IRepositoryManager _manager;

    public BookManager(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public IEnumerable<Book> GetAllBooks(bool trackChanges)
    {
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
        var entity = _manager.Book.GetOneBookById(book.Id,true);
        if (entity is null) throw new Exception($"Book with id:{book.Id} could not be found");

        _manager.Book.UpdateOneBook(book);
        _manager.Save();
    }

    public void DeleteOneBook(int id)
    {
        var entity = _manager.Book.GetOneBookById(id, true);
        if (entity is null) throw new Exception($"Book with id:{id} could not be found");

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