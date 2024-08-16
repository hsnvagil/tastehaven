using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Taste_Haven_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeedMenuItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[,]
                {
                    { 1, "Appetizer", "Crispy fried calamari served with marinara sauce.", "https://tastehavenimages.blob.core.windows.net/tastehaven/merlin_200483841_0edb972a-b86a-4f14-8983-1e50e4977584-superJumbo.jpg", "Fried Calamari", 12.99, "Popular" },
                    { 2, "Appetizer", "Freshly made guacamole with tortilla chips.", "https://tastehavenimages.blob.core.windows.net/tastehaven/guacamole-with-crispy-tortilla-chips-23734-1.webp", "Guacamole & Chips", 9.9900000000000002, "Vegan" },
                    { 3, "Main Course", "Grilled steak cooked to your liking with a side of mashed potatoes.", "https://tastehavenimages.blob.core.windows.net/tastehaven/grilled-flank-steak.jpg", "Grilled Steak", 24.989999999999998, "Signature" },
                    { 4, "Main Course", "Classic lasagna layered with beef, cheese, and marinara sauce.", "https://tastehavenimages.blob.core.windows.net/tastehaven/beef-lasagne-77009-1.jpeg", "Beef Lasagna", 14.99, "Popular" },
                    { 5, "Dessert", "Rich chocolate cake topped with a creamy ganache.", "https://tastehavenimages.blob.core.windows.net/tastehaven/easy_chocolate_cake_slice-500x375.jpg", "Chocolate Cake", 6.9900000000000002, "Sweet Tooth" },
                    { 6, "Dessert", "Creamy cheesecake with a graham cracker crust.", "https://tastehavenimages.blob.core.windows.net/tastehaven/Template-Size-for-Blog-Photos-24.jpg", "Cheesecake", 7.9900000000000002, "Classic" },
                    { 7, "Beverage", "Refreshing lemonade made from fresh lemons.", "https://tastehavenimages.blob.core.windows.net/tastehaven/lemonade.jpg", "Lemonade", 3.9900000000000002, "Refreshing" },
                    { 8, "Beverage", "Rich and bold coffee brewed to perfection.", "https://tastehavenimages.blob.core.windows.net/tastehaven/Latte_and_dark_coffee.jpg", "Coffee", 2.9900000000000002, "Hot" },
                    { 9, "Side", "Crispy and golden french fries.", "https://tastehavenimages.blob.core.windows.net/tastehaven/Copycat-McDonalds-French-Fries--500x375.webp", "French Fries", 4.9900000000000002, "Classic" },
                    { 10, "Side", "A fresh garden salad with a variety of vegetables.", "https://tastehavenimages.blob.core.windows.net/tastehaven/Garden-salad-thumbnail.jpg", "Garden Salad", 5.9900000000000002, "Healthy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
