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

    public UserDTO RegisterUser(string name, string email, string password)
    {
        try
        {
            var user = new User
            {
                Name = name,
                Email = email
            };

            var hash = _passwordHasher.HashPassword(user, password);

            user.PasswordHash = hash;

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

            _passwordHasher.VerifyHashedPassword(user, password);

            var token = _userTokenService.CreateToken(user);

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
}