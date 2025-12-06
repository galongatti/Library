using Library.Model.Entities;

namespace Library.Repository;

public interface IAuthorRepository
{
    Task<List<Author>> GetAuthorByNameAsync(string name);
    Task<List<Author>> GetAuthorsAsync();
    Task<Author> CreateAuthorAsync(Author author);
    Task<bool> UpdateAuthorAsync(Author author);
    Task<Author?> GetAuthorByIdAsync(int id);
    Task<List<Author>> GetAuthorsByIdsAsync(List<int> ids);
}