using Library.Model.DTO;
using Library.Model.Entities;

namespace Library.Services;

public interface IAuthorService
{
    Task<List<Author>> GetAuthorByName(string name);
    Task<List<Author>> GetAuthors();
    Task<Author> CreateAuthor(CreateAuthor author);
    Task<bool> UpdateAuthor(int id, UpdateAuthor author);
    Task<bool> DeleteAuthor(int id);
    Task<Author?> GetAuthorByIdNoTracking(int id);
}