namespace Taste_Haven_API.Models.Dto;

public record LoginRequestDTO
{
    public string UserName { get; set; }
    public string Password { get; set; }
}