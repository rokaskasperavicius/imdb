using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;
using Microsoft.IdentityModel.Tokens;

namespace IMDB_API.Infrastructure.Repositories;

public class UserTokenService : IUserTokenService
{
    private readonly IConfiguration _configuration;

    public UserTokenService(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(User user)
    {
        // Create token claims
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var secretKey = _configuration["JwtSecretKey"];

        var key =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes($"{secretKey}"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            "imdb",
            "imdb",
            claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        var access = new JwtSecurityTokenHandler().WriteToken(token);

        return access;
    }
}