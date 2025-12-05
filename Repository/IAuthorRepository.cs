using Library.Model.Entities;

namespace Library.Repository;

public interface IAuthorRepository
{
    Task<List<Author>> GetAuthorByName(string name);
    Task<List<Author>> GetAuthors();
    Task<Author> CreateAuthor(Author author);
    Task<bool> UpdateAuthor(Author author);
    Task<Author?> GetAuthorByIdTracking(int id);
    Task<Author?> GetAuthorByIdNoTracking(int id);
    Task<List<Author>> GetAuthorsByIds(List<int> ids);
}