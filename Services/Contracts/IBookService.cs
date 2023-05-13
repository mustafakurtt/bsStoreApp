using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Contracts;

public interface IBookService
{
    IEnumerable<Book> GetAllBooks(bool trackChanges);
    Book? GetOneBookById(int id, bool trackChanges);
    void CreateOneBook(Book book);
    void UpdateOneBook(int id,BookDtoForUpdate bookDto);
    void DeleteOneBook(int id);
    Book PartiallyUpdateOneBook(int id,JsonPatchDocument<Book> bookPatch);
}