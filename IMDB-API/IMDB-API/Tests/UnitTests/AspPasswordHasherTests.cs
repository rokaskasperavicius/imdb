using IMDB_API.Infrastructure.Common;
using Xunit;

namespace IMDB_API.Tests.UnitTests;

public class AspPasswordHasherTests
{
    [Fact]
    public void HashPassword_Returns_NonEmpty_Hash()
    {
        var hasher = new AspPasswordHasher();

        var hash = hasher.HashPassword("test@test.com", "password");

        Assert.False(string.IsNullOrWhiteSpace(hash));
        Assert.NotEqual("password", hash);
    }

    [Fact]
    public void VerifyHashedPassword_CorrectPassword_DoesNotThrow()
    {
        var hasher = new AspPasswordHasher();
        var hash = hasher.HashPassword("test@test.com", "password");

        hasher.VerifyHashedPassword("test@test.com", hash, "password");
    }

    [Fact]
    public void VerifyHashedPassword_WrongPassword_ThrowsUnauthorized()
    {
        var hasher = new AspPasswordHasher();
        var hash = hasher.HashPassword("test@test.com", "password");

        Assert.Throws<UnauthorizedAccessException>(() =>
            hasher.VerifyHashedPassword("test@test.com", hash,
                "wrong-password"));
    }
}