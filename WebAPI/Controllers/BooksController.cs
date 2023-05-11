using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Repositories.EFCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IRepositoryManager _manager;

        public BooksController(RepositoryContext repositoryContext, IRepositoryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _manager.Book.FindAll(false);
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBook([FromRoute(Name = "id")]int id)
        {
            var book = _manager.Book.GetOneBookById(id,false);
            if (book is null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody]Book book)
        {
            try
            {
                if (book is null) return BadRequest();
                _manager.Book.Create(book);
                _manager.Save();
                return StatusCode(201,book);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromBody] Book book, [FromRoute(Name = "id")] int id)
        {
            try
            {
                if (book is null) return BadRequest();
                if (id != book.Id) return BadRequest();

                var entity = _manager.Book.GetOneBookById(id, true);
                if (entity is null) return NotFound();

                entity.Title = book.Title;
                entity.Price = book.Price;

                _manager.Book.Update(book);
                _manager.Save();
                return StatusCode(201, entity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {

                var entity = _manager.Book.GetOneBookById(id, true);
                if (entity is null) return NotFound();

                _manager.Book.Delete(entity);
                _manager.Save();
                return StatusCode(201, entity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            try
            {

                var entity = _manager.Book.GetOneBookById(id, true);
                if (entity is null) return NotFound();

                bookPatch.ApplyTo(entity);
                _manager.Book.Update(entity);
                _manager.Save();
                return StatusCode(201, entity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
