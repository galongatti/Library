using Library.Model.DTO;
using Library.Model.Entities;

namespace Library.Services;

public interface IAuthorService
{
    Task<List<Author>> GetAuthorByNameAsync(string name);
    Task<List<Author>> GetAuthorsAsync();
    Task<Author> CreateAuthorAsync(CreateAuthor author);
    Task<bool> UpdateAuthorAsync(int id, UpdateAuthor author);
    Task<bool> DeleteAuthorAsync(int id);
    Task<Author?> GetAuthorByIdAsync(int id);
}