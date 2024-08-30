namespace Taste_Haven_API.Models.Dto;

public record MenuItemUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SpecialTag { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public IFormFile File { get; set; }
}