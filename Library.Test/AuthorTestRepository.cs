using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Context;
using Library.Model.Entities;
using Library.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Library.Test;

public class AuthorTestRepository
{
    private AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    private async Task SeedAuthorsAsync(DbContext context)
    {
        var authors = new List<Author>
        {
            new("Author 11", id: 1),
            new("Author 12", id: 2),
            new("Author 3", id: 3),
            new("Another Author 3", id: 4),
        };

        await context.Set<Author>().AddRangeAsync(authors);
        await context.SaveChangesAsync();
    }


    [Fact(DisplayName = "Should return all authors")]
    public async Task GetAllAuthorsAsync_ShouldReturnAllAuthors()
    {
        // arrange
        using var context = CreateDbContext();
        await SeedAuthorsAsync(context);
        var repository = new AuthorRepository(context);

        // act
        var result = await repository.GetAuthorsAsync();

        // assert
        Assert.Equal(4, result.Count);
    }

    [Fact(DisplayName = "Should create a new author")]
    public async Task CreateAuthorAsync_ShouldCreateNewAuthor()
    {
        //arrange
        using var context = CreateDbContext();
        var repository = new AuthorRepository(context);
        var newAuthor = new Author("New Author");

        //act
        var result = await repository.CreateAuthorAsync(newAuthor);

        //assert
        Assert.NotEqual(0, result.Id);
    }

    [Fact(DisplayName = "Should search authors by name with LIKE Author 1")]
    public async Task GetAuthorByNameAsync_ShouldReturnMatchingAuthors()
    {
        //arrange
        using var context = CreateDbContext();
        await SeedAuthorsAsync(context);

        var repository = new AuthorRepository(context);
        var nameToSearch = "Author 1";

        //act
        var result = await repository.GetAuthorByNameAsync(nameToSearch);

        //assert
        Assert.Equal(2, result.Count);
    }

    [Fact(DisplayName = "Should return author by name with LIKE Author 3")]
    public async Task GetAuthorByNameAsync_ShouldReturnAuthorsWithAuthor3InName()
    {
        //arrange
        using var dbContext = CreateDbContext();
        await SeedAuthorsAsync(dbContext);
        var repository = new AuthorRepository(dbContext);
        var nameToSearch = "Author 3";

        //act
        var result = await repository.GetAuthorByNameAsync(nameToSearch);

        //assert

        result.ForEach(r => { Assert.Contains("Author 3", r.Name); });
    }

    [Fact(DisplayName = "Should update an existing author")]
    public async Task UpdateAuthorAsync_ShouldUpdateExistingAuthor()
    {
        // arrange
        using var context = CreateDbContext();
        await SeedAuthorsAsync(context);

        var repository = new AuthorRepository(context);
        var authors = await repository.GetAuthorsAsync();

        var selectedAuthor = authors.First();
        var id = selectedAuthor.Id;
        var newName = "New Name";

        var authorToUpdate = new Author(selectedAuthor.Name, id);
        authorToUpdate.Update(newName);

        // act
        var updateResult = await repository.UpdateAuthorAsync(authorToUpdate);
        var updatedAuthor = await repository.GetAuthorByIdAsync(id);

        // assert
        Assert.True(updateResult);
        Assert.Equal(newName, updatedAuthor?.Name);
    }

    [Fact(DisplayName = "Should return authors by given an id list")]
    public async Task GetAuthorsByIdsAsync_ShouldReturnAuthorsByGivenIdList()
    {
        //arrange
        var context = CreateDbContext();
        await SeedAuthorsAsync(context);
        var repository = new AuthorRepository(context);
        var ids = new List<int> { 1, 2 };

        //act
        var response = repository.GetAuthorsByIdsAsync(ids);

        //assert
        Assert.Equal(2, response.Result.Count);
    }
}