using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.EFCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;

        public BooksController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _repositoryContext.Books.ToList();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBook([FromRoute(Name = "id")]int id)
        {
            var book = _repositoryContext.Books.SingleOrDefault(b => b.Id.Equals(id));
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
                _repositoryContext.Books.Add(book);
                _repositoryContext.SaveChanges();
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

                var entity = _repositoryContext.Books.SingleOrDefault(b => b.Id.Equals(id));
                if (entity is null) return NotFound();

                entity.Title = book.Title;
                entity.Price = book.Price;

                _repositoryContext.Books.Update(entity);
                _repositoryContext.SaveChanges();
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

                var entity = _repositoryContext.Books.SingleOrDefault(b => b.Id.Equals(id));
                if (entity is null) return NotFound();

                _repositoryContext.Books.Remove(entity);
                _repositoryContext.SaveChanges();
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

                var entity = _repositoryContext.Books.SingleOrDefault(b => b.Id.Equals(id));
                if (entity is null) return NotFound();

                bookPatch.ApplyTo(entity);
                _repositoryContext.Update(entity);
                _repositoryContext.SaveChanges();
                return StatusCode(201, entity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
