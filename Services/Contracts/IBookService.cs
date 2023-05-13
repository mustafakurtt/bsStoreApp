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
    Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges);
    Task<Book> GetOneBookByIdAsync(int id, bool trackChanges);
    Task<BookDto> CreateOneBookAsync(Book book);
    Task UpdateOneBookAsync(int id,BookDtoForUpdate bookDto);
    Task DeleteOneBookAsync(int id);
    Task<Book> PartiallyUpdateOneBookAsync(int id,JsonPatchDocument<Book> bookPatch);
}