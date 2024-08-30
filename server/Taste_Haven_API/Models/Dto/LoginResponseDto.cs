namespace Taste_Haven_API.Models.Dto;

public record LoginResponseDto
{
    public string Email { get; set; }
    public string Token { get; set; }
}