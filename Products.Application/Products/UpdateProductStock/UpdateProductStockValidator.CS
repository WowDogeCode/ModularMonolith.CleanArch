using FluentValidation;
using Products.Application.Abstraction.Repositories;

namespace Products.Application.Products.UpdateProductStock
{
    public sealed class UpdateProductStockValidator : AbstractValidator<UpdateProductStockCommand>
    {
        public UpdateProductStockValidator()
        {
            RuleFor(x => x.Stock)
                .GreaterThan((short)0)
                .WithMessage("New stock amount must be more than 0");

            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Product id can't be empty");
        }
    }
}
