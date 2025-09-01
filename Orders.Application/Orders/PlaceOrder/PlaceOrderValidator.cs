using FluentValidation;
using Orders.Application.Orders.PlaceOrder;

namespace Orders.Application.Orders.PlaceOrder
{
    public sealed class PlaceOrderValidator : AbstractValidator<PlaceOrderCommand>
    {
        public PlaceOrderValidator()
        {
            RuleFor(x => x.CustomerId)
                .MinimumLength(0)
                .When(x => !string.IsNullOrEmpty(x.CustomerId))
                .WithMessage("Customer id must be longer than 0 character");

            RuleFor(x => x.EmployeeId)
                .GreaterThan(0)
                .When(x => x.EmployeeId.HasValue)
                .WithMessage("Employee id must be greater than 0");

            RuleFor(x => x.ShipVia)
                .GreaterThan(0)
                .When(x => x.ShipVia.HasValue)
                .WithMessage("Ship via must be greater than 0");

            RuleFor(x => x.OrderDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Order date cannot be in the future");

            RuleFor(x => x.ShipAddress)
                .NotEmpty()
                .WithMessage("Shipping address cannot be empty");

            RuleFor(x => x.RequiredDate)
                .GreaterThan(x => x.OrderDate)
                .WithMessage("Required date must be after order date");

            RuleFor(x => x.ShippedDate)
                .GreaterThan(x => x.OrderDate)
                .When(x => x.ShippedDate.HasValue)
                .WithMessage("Shipped date must be after order date");

            RuleFor(x => x.Freight)
                .GreaterThan(0)
                .WithMessage("Freight cannot be less than 0");

            RuleFor(x => x.ShipName)
                .Length(1, 100)
                .WithMessage("Ship name must be between 1 and 100 characters");

            RuleFor(x => x.ShipAddress)
                .Length(1, 200)
                .WithMessage("Ship address must be between 1 and 200 characters");

            RuleFor(x => x.ShipCity)
                .Length(1, 100)
                .WithMessage("Ship city must be between 1 and 100 characters");

            RuleFor(x => x.ShipRegion)
                .MaximumLength(100)
                .WithMessage("Ship region length must be less than 100")
                .When(x => !string.IsNullOrEmpty(x.ShipRegion));

            RuleFor(x => x.ShipPostalCode)
                .Matches(@"^\d{5}(-\d{4})?$")
                .WithMessage("Postal code must be 5 digits or 5+4 format (e.g. 12345 or 12345-6789).")
                .When(x => !string.IsNullOrWhiteSpace(x.ShipPostalCode));

            RuleFor(x => x.ShipCountry)
                .NotEmpty()
                .WithMessage("Ship country is required")
                .Matches(@"^[A-Za-z\s]+$")
                .WithMessage("Ship country must contain only letters and spaces");
        }
    }
}