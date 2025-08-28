using FluentValidation;

namespace Products.Application.Products.ReduceStock
{
    public sealed class ReduceStockValidator : AbstractValidator<ReduceStockCommand>
    {
        public ReduceStockValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Product id value must be greater than 0");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity value must be greater than 0");
        }
    }
}
