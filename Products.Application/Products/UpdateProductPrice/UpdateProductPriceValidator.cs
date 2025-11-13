using FluentValidation;

namespace Products.Application.Products.UpdateProductPrice
{
    public sealed class UpdateProductPriceValidator : AbstractValidator<UpdateProductPriceCommand>
    {
        public UpdateProductPriceValidator()
        {
            RuleFor(x => x.Price).InclusiveBetween(1, 10000).WithMessage("Product's new price must be between 0 and 10000");
        }
    }
}
