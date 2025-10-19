using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IMDB_API.Application.Interfaces;
using IMDB_API.Contracts.DTOs;
using IMDB_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace IMDB_API.Application.Services;

public class UsersService : IUsersService
{
    private readonly IConfiguration _configuration;
    private readonly IUsersRepository _usersRepository;

    public UsersService(
        IUsersRepository usersRepository,
        IConfiguration configuration)
    {
        _usersRepository = usersRepository;
        _configuration = configuration;
    }

    public UserDTO RegisterUser(string name, string email, string password)
    {
        try
        {
            var passwordHasher = new PasswordHasher<string>();
            var hash = passwordHasher.HashPassword(email, password);

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = hash
            };

            var createdUser = _usersRepository.CreateUser(user);

            return new UserDTO
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Email = createdUser.Email
            };
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("User already exists", ex);
        }
    }

    public UserWithTokenDTO LoginUser(string email, string password)
    {
        try
        {
            var user = _usersRepository.GetUserByEmail(email);

            var passwordHasher = new PasswordHasher<string>();
            var hashResult = passwordHasher.VerifyHashedPassword(
                email,
                user.PasswordHash,
                password);

            // Check hash
            if (hashResult == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException();

            var token = CreateToken(user);

            return new UserWithTokenDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                AccessToken = token
            };
        }
        catch (Exception ex)
        {
            throw new UnauthorizedAccessException(
                "Wrong email or password", ex);
        }
    }

    private string CreateToken(User user)
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
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds);

        var access = new JwtSecurityTokenHandler().WriteToken(token);

        return access;
    }
}