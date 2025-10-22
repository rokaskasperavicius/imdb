using IMDB_API.Domain;
using IMDB_API.Infrastructure.Repositories;
using Xunit;

public class AspPasswordHasherTests
{
    [Fact]
    public void HashPassword_Returns_NonEmpty_Hash()
    {
        var hasher = new AspPasswordHasher();
        var user = new User { Email = "test@test.com" };

        var hash = hasher.HashPassword(user, "password");

        Assert.False(string.IsNullOrWhiteSpace(hash));
        Assert.NotEqual("password", hash);
    }

    [Fact]
    public void VerifyHashedPassword_CorrectPassword_DoesNotThrow()
    {
        var hasher = new AspPasswordHasher();
        var user = new User { Email = "test@test.com" };
        var hash = hasher.HashPassword(user, "password");
        user.PasswordHash = hash;

        hasher.VerifyHashedPassword(user, "password");
    }

    [Fact]
    public void VerifyHashedPassword_WrongPassword_ThrowsUnauthorized()
    {
        var hasher = new AspPasswordHasher();
        var user = new User { Email = "test@test.com" };
        var hash = hasher.HashPassword(user, "password");
        user.PasswordHash = hash;

        Assert.Throws<UnauthorizedAccessException>(() =>
            hasher.VerifyHashedPassword(user, "wrong-password"));
    }
}