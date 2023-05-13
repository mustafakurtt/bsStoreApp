using Entities.Dtos;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public BooksController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public IActionResult GetAllBooks()
    {
        var books = _serviceManager.BookService.GetAllBooksAsync(false);
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetBookById([FromRoute(Name = "id")] int id)
    {
        var book = _serviceManager.BookService.GetOneBookByIdAsync(id, false);
        return Ok(book);
    }

    [HttpPost]
    public IActionResult CreateOneBook([FromBody] Book book)
    {
        _serviceManager.BookService.CreateOneBookAsync(book);
        return StatusCode(201, book);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateOneBook([FromBody] BookDtoForUpdate bookDto, [FromRoute(Name = "id")] int id)
    {
      
        if (id != bookDto.Id) throw new Exception("Id's not matched");
        _serviceManager.BookService.UpdateOneBookAsync(id,bookDto);
        return Ok();
        
    }
    [HttpDelete("{id:int}")]
    public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
    {
       _serviceManager.BookService.DeleteOneBookAsync(id);
       return Ok();
    }

    [HttpPatch("{id:int}")]
    public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
    {
        var book = _serviceManager.BookService.PartiallyUpdateOneBookAsync(id, bookPatch);
        return Ok(book);
    }
}