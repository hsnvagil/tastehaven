#region

using FluentValidation;
using Taste_Haven_API.Models.Dto;

#endregion

namespace Taste_Haven_API.Validation;

public class AddMenuItemValidator : AbstractValidator<MenuItemCreateDto>
{
    public AddMenuItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name field cannot be empty.");

        RuleFor(x => x.File)
            .NotEmpty()
            .WithMessage("File must be provided.")
            .Must(file => file?.ContentType is "image/jpeg" or "image/png")
            .WithMessage("Only JPEG and PNG files are allowed.");

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price field cannot be empty")
            .GreaterThan(0)
            .WithMessage("Value must be greater than 0.");
    }
}

public class UpdateMenuItemValidator : AbstractValidator<MenuItemUpdateDto>
{
    public UpdateMenuItemValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name field cannot be empty.");

        RuleFor(x => x.File)
            .NotEmpty()
            .WithMessage("File must be provided.")
            .Must(file => file?.ContentType is "image/jpeg" or "image/png")
            .WithMessage("Only JPEG and PNG files are allowed.");

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price field cannot be empty")
            .GreaterThan(0)
            .WithMessage("Value must be greater than 0.");
    }
}

public class CreateOrderDetailsValidator : AbstractValidator<OrderHeaderCreateDto>
{
    public CreateOrderDetailsValidator()
    {
        RuleFor(x => x.PickupName)
            .NotNull()
            .NotEmpty()
            .WithMessage("Pickup name field cannot be empty");

        RuleFor(x => x.PickupPhoneNumber)
            .NotNull()
            .WithMessage("Pickup phone number field cannot be empty");

        RuleFor(x => x.PickupEmail)
            .NotNull()
            .WithMessage("Pickup email field cannot be empty");

        RuleFor(x => x.OrderTotal)
            .NotNull()
            .WithMessage("Order total field cannot be empty");

        RuleFor(x => x.TotalItems)
            .NotNull()
            .WithMessage("Total items field cannot be empty");

    }
}

public class RegisterValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Name field cannot be empty");

        RuleFor(x => x.UserName)
            .NotNull()
            .WithMessage("Username field cannot be empty");

        RuleFor(x => x.Role)
            .NotNull()
            .WithMessage("Role field cannot be empty");

        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("Password field cannot be empty");
    }
}

public class LoginValidator : AbstractValidator<LoginRequestDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.UserName)
            .NotNull()
            .WithMessage("Username field cannot be empty");

        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("Password field cannot be empty");
    }
}



