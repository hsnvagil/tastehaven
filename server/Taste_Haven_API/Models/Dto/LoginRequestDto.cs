namespace Taste_Haven_API.Models.Dto;

public record LoginRequestDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}