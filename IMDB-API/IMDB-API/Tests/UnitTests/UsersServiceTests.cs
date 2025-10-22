using IMDB_API.Application.Interfaces;
using IMDB_API.Application.Services;
using IMDB_API.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace IMDB_API.Tests.UnitTests;

public class UsersServiceTests
{
    private readonly Dictionary<string, string?> _inMemorySettings = new()
    {
        { "JwtSecretKey", "--super-secret-key-32-bit-value--" }
    };

    private readonly Mock<IUsersRepository> _repo = new();

    private UsersService CreateService()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(_inMemorySettings)
            .Build();

        return new UsersService(_repo.Object, configuration);
    }

    [Fact]
    public void LoginUser_WrongPassword_ThrowsUnauthorized()
    {
        var email = "test@test.com";
        var hasher = new PasswordHasher<string>();
        var hash = hasher.HashPassword(email, "correct");

        _repo.Setup(r => r.GetUserByEmail(email)).Returns(new User
        {
            Id = 1,
            Email = email,
            PasswordHash = hash
        });

        var userService = CreateService();

        Assert.Throws<UnauthorizedAccessException>(() =>
            userService.LoginUser(email, "wrong"));
    }
}