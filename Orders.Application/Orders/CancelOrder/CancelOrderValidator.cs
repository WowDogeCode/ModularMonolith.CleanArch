using FluentValidation;

namespace Orders.Application.Orders.CancelOrder
{
    public sealed class CancelOrderValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0)
                .WithMessage("Order id must be greater than 0");
        }
    }
}
