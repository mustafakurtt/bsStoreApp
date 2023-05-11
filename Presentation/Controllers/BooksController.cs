﻿using Entities.Models;
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
        var books = _serviceManager.BookService.GetAllBooks(false);
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetBookById([FromRoute(Name = "id")] int id)
    {
        var book = _serviceManager.BookService.GetOneBookById(id, false);
        return Ok(book);
    }

    [HttpPost]
    public IActionResult CreateOneBook([FromBody] Book book)
    {
        _serviceManager.BookService.CreateOneBook(book);
        return StatusCode(201, book);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateOneBook([FromBody] Book book, [FromRoute(Name = "id")] int id)
    {
        try
        {
            _serviceManager.BookService.UpdateOneBook(book);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }
    [HttpDelete("{id:int}")]
    public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
    {
       _serviceManager.BookService.DeleteOneBook(id);
       return Ok();
    }

    [HttpPatch("{id:int}")]
    public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
    {
        var book = _serviceManager.BookService.PartiallyUpdateOneBook(id, bookPatch);
        return Ok(book);
    }
}