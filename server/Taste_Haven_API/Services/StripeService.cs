#region

using Stripe;
using Taste_Haven_API.Models;

#endregion

namespace Taste_Haven_API.Services;

public interface IStripeService
{
    PaymentIntent CreatePaymentIntent(ShoppingCart shoppingCart);
}

public class StripeService : IStripeService
{
    public StripeService(IConfiguration configuration)
    {
        StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];
    }

    public PaymentIntent CreatePaymentIntent(ShoppingCart shoppingCart)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = (int)(shoppingCart.CartTotal * 100),
            Currency = "usd",
            PaymentMethodTypes = ["card"]
        };
        var service = new PaymentIntentService();
        return service.Create(options);
    }
}