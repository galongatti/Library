using Library.Exceptions;
using Library.Model.DTO;
using Library.Model.Entities;
using Library.Repository;

namespace Library.Services;

public class AuthorService(IAuthorRepository repository) : IAuthorService
{
    public Task<List<Author>> GetAuthorByNameAsync(string name)
    {
        return repository.GetAuthorByNameAsync(name);
    }

    public async Task<List<Author>> GetAuthorsAsync()
    {
        return await repository.GetAuthorsAsync();
    }

    public async Task<Author> CreateAuthorAsync(CreateAuthor createAuthor)
    {
        Author author = createAuthor.ToEntity();
        return await repository.CreateAuthorAsync(author);      
    }

    public async Task<bool> UpdateAuthorAsync(int id, UpdateAuthor author)
    {
        Author? authorExist = await repository.GetAuthorByIdAsync(id);
        
        if( authorExist is null)
            throw new AuthorException("Author not found");
        
        authorExist.Update(author.Name);
        
        return await repository.UpdateAuthorAsync(authorExist);
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        Author? authorExist = await repository.GetAuthorByIdAsync(id);
        
        if( authorExist is null)
            throw new AuthorException("Author not found");
        
        authorExist.SetAsDeleted();
        
        return await repository.UpdateAuthorAsync(authorExist);
    }

    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        Author? author = await repository.GetAuthorByIdAsync(id);
        
        return author ?? throw new AuthorException("Author not found");
    }
}