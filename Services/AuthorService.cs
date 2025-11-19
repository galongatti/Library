using Library.Model.DTO;
using Library.Model.Entities;
using Library.Repository;

namespace Library.Services;

public class AuthorService(IAuthorRepository repository) : IAuthorService
{
    public Task<List<Author>> GetAuthorByName(string name)
    {
        return repository.GetAuthorByName(name);
    }

    public async Task<List<Author>> GetAuthors()
    {
        return await repository.GetAuthors();
    }

    public async Task<Author> CreateAuthor(CreateAuthor createAuthor)
    {
        Author author = createAuthor.ToEntity();
        return await repository.CreateAuthor(author);      
    }

    public async Task<bool> UpdateAuthor(int id, UpdateAuthor author)
    {

        Author? authorExist = await repository.GetAuthorByIdTracking(id);
        
        if( authorExist is null)
            throw new Exception("Author not found");
        
        authorExist.Update(author.Name);
        
        return await repository.UpdateAuthor(authorExist);
    }

    public async Task<bool> DeleteAuthor(int id)
    {
        Author? authorExist = await repository.GetAuthorByIdTracking(id);
        
        if( authorExist is null)
            throw new Exception("Author not found");
        
        authorExist.SetAsDeleted();
        
        return await repository.UpdateAuthor(authorExist);
    }

    public Task<Author?> GetAuthorByIdNoTracking(int id)
    {
        return repository.GetAuthorByIdNoTracking(id);
    }
}