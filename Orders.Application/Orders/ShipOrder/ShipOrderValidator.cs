using FluentValidation;

namespace Orders.Application.Orders.ShipOrder
{
    public sealed class ShipOrderValidator : AbstractValidator<ShipOrderCommand>
    {
        public ShipOrderValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("Order id must be greater than 0");
        }
    }
}
