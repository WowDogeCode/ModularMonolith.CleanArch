using FluentValidation;

namespace Products.Application.Products.AddProduct
{
    public sealed class AddProductValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name is required")
                .Length(3, 100).WithMessage("Product name length must be between 3 and 100 characters")
                .Must(name => !(name.Contains('@') || name.Contains('#') || name.Contains('!')))
                .WithMessage("Product name cannot contain special characters @, #, !");
          
            RuleFor(x => x.UnitPrice)
                .InclusiveBetween(0, 10000)
                .WithMessage("Price must be between 0 and 10000");

            RuleFor(x => x.UnitsInStock)
                .GreaterThanOrEqualTo((short)0)
                .WithMessage("Units in stock must be greater or equal to 0");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("Category id must be greater than 0");
        }
    }
}
