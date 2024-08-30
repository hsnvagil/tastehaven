#region

using Taste_Haven_API.Models;
using Taste_Haven_API.Repositories;

#endregion

namespace Taste_Haven_API.Services;

public interface IPaymentService
{
    Task<ShoppingCart> ProcessPayment(string userId);
}

public class PaymentService(IPaymentRepository paymentRepository, IStripeService stripeService)
    : IPaymentService
{
    public async Task<ShoppingCart> ProcessPayment(string userId)
    {
        var shoppingCart = await paymentRepository.GetShoppingCartByUserIdAsync(userId);

        if (shoppingCart?.CartItems == null || shoppingCart.CartItems.Count == 0)
            throw new Exception("Shopping cart is empty or not found.");

        shoppingCart.CartTotal = shoppingCart.CartItems.Sum(u => u.Quantity * u.MenuItem.Price);
        var paymentIntent = stripeService.CreatePaymentIntent(shoppingCart);
        shoppingCart.StripePaymentIntentId = paymentIntent.Id;
        shoppingCart.ClientSecret = paymentIntent.ClientSecret;

        await paymentRepository.SaveChangesAsync();

        return shoppingCart;
    }
}