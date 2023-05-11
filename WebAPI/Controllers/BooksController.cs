using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        //    private readonly IServiceManager _manager;

        //    public BooksController(IServiceManager manager)
        //    {
        //        _manager = manager;
        //    }

        //    [HttpGet]
        //    public IActionResult GetAllBooks()
        //    {
        //        var books = _manager.BookService.GetAllBooks(false);
        //        return Ok(books);
        //    }

        //    [HttpGet("{id:int}")]
        //    public IActionResult GetBook([FromRoute(Name = "id")]int id)
        //    {
        //        var book = _manager.BookService.GetOneBookById(id, false);
        //        if (book is null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(book);
        //    }

        //    [HttpPost]
        //    public IActionResult CreateOneBook([FromBody]Book book)
        //    {
        //        try
        //        {
        //            if (book is null) return BadRequest();
        //            _manager.BookService.CreateOneBook(book);
        //            return StatusCode(201,book);
        //        }
        //        catch (Exception e)
        //        {
        //            return BadRequest(e.Message);
        //        }
        //    }

        //    [HttpPut("{id:int}")]
        //    public IActionResult UpdateOneBook([FromBody] Book book, [FromRoute(Name = "id")] int id)
        //    {
        //        try
        //        {
        //            if (book is null) return BadRequest();
        //            if (id != book.Id) return BadRequest();

        //            _manager.BookService.UpdateOneBook(book.Id, book, true);
        //            return NoContent();
        //        }
        //        catch (Exception e)
        //        {
        //            return BadRequest(e.Message);
        //        }
        //    }

        //    [HttpDelete("{id:int}")]
        //    public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        //    {
        //        try
        //        {

        //            var entity = _manager.BookService.GetOneBookById(id, false);
        //            if (entity is null) return NotFound();

        //            _manager.BookService.DeleteOneBook(id);
        //            return NoContent();
        //        }
        //        catch (Exception e)
        //        {
        //            return BadRequest(e.Message);
        //        }
        //    }

        //    [HttpPatch("{id:int}")]
        //    public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        //    {
        //        try
        //        {

        //            var entity = _manager.BookService.GetOneBookById(id, true);
        //            if (entity is null) return NotFound();

        //            bookPatch.ApplyTo(entity);
        //            _manager.BookService.UpdateOneBook(id,entity,true);
        //            return StatusCode(201, entity);
        //        }
        //        catch (Exception e)
        //        {
        //            return BadRequest(e.Message);
        //        }
        //    }
    }
}
