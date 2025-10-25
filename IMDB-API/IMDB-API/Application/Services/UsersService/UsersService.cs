using IMDB_API.Application.DTOs;
using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;

namespace IMDB_API.Application.Services;

public class UsersService : IUsersService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _usersRepository;
    private readonly IUserTokenService _userTokenService;

    public UsersService(
        IUsersRepository usersRepository,
        IPasswordHasher passwordHasher,
        IUserTokenService userTokenService)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _userTokenService = userTokenService;
    }

    public UserDto RegisterUser(string name, string email, string password)
    {
        try
        {
            var hash = _passwordHasher.HashPassword(email, password);

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = hash
            };

            var createdUser = _usersRepository.CreateUser(user);

            return new UserDto
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

    public UserWithTokenDto LoginUser(string email, string password)
    {
        try
        {
            var user = _usersRepository.GetUserByEmail(email);

            _passwordHasher.VerifyHashedPassword(user.Email, user.PasswordHash,
                password);

            var token = _userTokenService.CreateToken(user);

            return new UserWithTokenDto
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
}