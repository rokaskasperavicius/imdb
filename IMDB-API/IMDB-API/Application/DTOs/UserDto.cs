namespace IMDB_API.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class UserWithTokenDto : UserDto
{
    public string AccessToken { get; set; }
}