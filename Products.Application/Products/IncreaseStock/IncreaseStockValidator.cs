using FluentValidation;

namespace Products.Application.Products.IncreaseStock
{
    public sealed class IncreaseStockValidator : AbstractValidator<IncreaseStockCommand>
    {
        public IncreaseStockValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Product id must be greater than 0");

            RuleFor(x => (int)x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");
        }
    }
}
