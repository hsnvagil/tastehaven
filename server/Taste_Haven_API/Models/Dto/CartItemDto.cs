namespace Taste_Haven_API.Models.Dto;

public class CartItemDto
{
    public int Id { get; set; }
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public MenuItemDto MenuItem { get; set; }
}