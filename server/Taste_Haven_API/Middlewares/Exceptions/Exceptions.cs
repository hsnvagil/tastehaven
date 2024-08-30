namespace Taste_Haven_API.Middlewares.Exceptions;

public class InvalidMenuItemIdException() : Exception("Invalid menu item ID.");

public class MenuItemNotFoundException() : Exception("Menu item not found.");

public class InvalidOrderIdException() : Exception("Invalid order ID");

public class OrderNotFoundException() : Exception("Order not found.");