namespace IMDB_API.Application.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class UserWithTokenDTO : UserDTO
{
    public string AccessToken { get; set; }
}