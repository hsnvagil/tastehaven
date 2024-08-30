namespace Taste_Haven_API.Models.Dto;

public record RegisterRequestDto
{
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}