using IMDB_API.Application.Services;
using IMDB_API.Infrastructure;
using IMDB_API.Infrastructure.Models;
using IMDB_API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IMDB_API.Tests.IntegrationTests;

public class PeopleServiceTests
{
    private ImdbDbContext CreateContext()
    {
        return new ImdbDbContext(
            new DbContextOptionsBuilder<ImdbDbContext>()
                .UseInMemoryDatabase("ImdbTestDB")
                .Options);
    }

    [Fact]
    public async Task GetActors_Success_ReturnsActorsFromDatabase()
    {
        await using (var context = CreateContext())
        {
            // Fill DB with data
            await context.Database.EnsureCreatedAsync();
            context.Names.Add(new Name
            {
                Nconst = "nm00001",
                Primaryname = "Test Name",
                Birthyear = "1975",
                Deathyear = null,
                Rating = 9
            });
            await context.SaveChangesAsync();

            // Initialize results
            var repo = new PeopleRepository(context);
            var service = new PeopleService(repo);
            var result = await service.GetPeople(1, 20);

            // Assert
            Assert.Single(result.Data);
            Assert.Contains(result.Data, m => m.PrimaryName == "Test Name");

            // Clean-up
            await context.Database.EnsureDeletedAsync();
        }
    }
}