namespace Taste_Haven_API.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class CartItem
{
    public int Id { get; set; }
    public int MenuItemId { get; set; }
    [ForeignKey("MenuItemId")]
    public MenuItem MenuItem { get; set; } = new();
    public int Quantity { get; set; }
    public int ShoppingCartId { get; set; }
}