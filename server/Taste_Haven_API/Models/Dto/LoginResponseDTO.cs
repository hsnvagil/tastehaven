namespace Taste_Haven_API.Models.Dto;

public record LoginResponseDTO
{
    public string Email { get; set; }
    public string Token { get; set; }
}