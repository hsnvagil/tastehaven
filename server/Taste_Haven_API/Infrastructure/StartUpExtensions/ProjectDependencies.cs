#region

using Taste_Haven_API.Repositories;
using Taste_Haven_API.Services;

#endregion

namespace Taste_Haven_API.Infrastructure.StartUpExtensions;

public static class ProjectDependencies
{
    public static IServiceCollection AddProjectDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IBlobService, BlobService>();
        services.AddSingleton<IJwtService, JwtService>();
        services.AddSingleton<IStripeService, StripeService>();
        
        services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IShoppingCartService, ShoppingCartService>();
       
        return services;
    }
}