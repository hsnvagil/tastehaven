using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taste_Haven_API.Models;

namespace Taste_Haven_API.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
	public ApplicationDbContext(DbContextOptions options) : base(options)
	{

	}

	public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	public DbSet<MenuItem> MenuItems { get; set; }
	
	public DbSet<ShoppingCart> ShoppingCarts { get; set; }
	public DbSet<CartItem> CartItems { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.Entity<MenuItem>().HasData(
		 new MenuItem
		 {
			 Id = 1,
			 Category = "Appetizer",
			 Description = "Crispy fried calamari served with marinara sauce.",
			 Image = "https://tastehavenimages.blob.core.windows.net/tastehaven/merlin_200483841_0edb972a-b86a-4f14-8983-1e50e4977584-superJumbo.jpg",
			 Name = "Fried Calamari",
			 Price = 12.99,
			 SpecialTag = "Popular"
		 },
		new MenuItem
		{
			Id = 2,
			Category = "Appetizer",
			Description = "Freshly made guacamole with tortilla chips.",
			Image = "https://tastehavenimages.blob.core.windows.net/tastehaven/guacamole-with-crispy-tortilla-chips-23734-1.webp",
			Name = "Guacamole & Chips",
			Price = 9.99,
			SpecialTag = "Vegan"
		},
		new MenuItem
		{
			Id = 3,
			Category = "Main Course",
			Description = "Grilled steak cooked to your liking with a side of mashed potatoes.",
			Image = "https://tastehavenimages.blob.core.windows.net/tastehaven/grilled-flank-steak.jpg",
			Name = "Grilled Steak",
			Price = 24.99,
			SpecialTag = "Signature"
		},
		new MenuItem
		{
			Id = 4,
			Category = "Main Course",
			Description = "Classic lasagna layered with beef, cheese, and marinara sauce.",
			Image = "https://tastehavenimages.blob.core.windows.net/tastehaven/beef-lasagne-77009-1.jpeg",
			Name = "Beef Lasagna",
			Price = 14.99,
			SpecialTag = "Popular"
		},
		new MenuItem
		{
			Id = 5,
			Category = "Dessert",
			Description = "Rich chocolate cake topped with a creamy ganache.",
			Image = "https://tastehavenimages.blob.core.windows.net/tastehaven/easy_chocolate_cake_slice-500x375.jpg",
			Name = "Chocolate Cake",
			Price = 6.99,
			SpecialTag = "Sweet Tooth"
		},
		new MenuItem
		{
			Id = 6,
			Category = "Dessert",
			Description = "Creamy cheesecake with a graham cracker crust.",
			Image = "https://tastehavenimages.blob.core.windows.net/tastehaven/Template-Size-for-Blog-Photos-24.jpg",
			Name = "Cheesecake",
			Price = 7.99,
			SpecialTag = "Classic"
		},
		new MenuItem
		{
			Id = 7,
			Category = "Beverage",
			Description = "Refreshing lemonade made from fresh lemons.",
			Image = "https://tastehavenimages.blob.core.windows.net/tastehaven/lemonade.jpg",
			Name = "Lemonade",
			Price = 3.99,
			SpecialTag = "Refreshing"
		},
		new MenuItem
		{
			Id = 8,
			Category = "Beverage",
			Description = "Rich and bold coffee brewed to perfection.",
			Image = "https://tastehavenimages.blob.core.windows.net/tastehaven/Latte_and_dark_coffee.jpg",
			Name = "Coffee",
			Price = 2.99,
			SpecialTag = "Hot"
		},
		new MenuItem
		{
			Id = 9,
			Category = "Side",
			Description = "Crispy and golden french fries.",
			Image = "https://tastehavenimages.blob.core.windows.net/tastehaven/Copycat-McDonalds-French-Fries--500x375.webp",
			Name = "French Fries",
			Price = 4.99,
			SpecialTag = "Classic"
		},
		new MenuItem
		{
			Id = 10,
			Category = "Side",
			Description = "A fresh garden salad with a variety of vegetables.",
			Image = "https://tastehavenimages.blob.core.windows.net/tastehaven/Garden-salad-thumbnail.jpg",
			Name = "Garden Salad",
			Price = 5.99,
			SpecialTag = "Healthy"
		}
	);
	}
}