namespace Taste_Haven_API.Models.Dto;

public class ShoppingCartDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public decimal CartTotal { get; set; }
    public List<CartItemDto> CartItems { get; set; }
}